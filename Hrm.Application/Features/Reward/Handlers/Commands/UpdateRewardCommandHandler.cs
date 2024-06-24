using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reward.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Reward.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reward.Handlers.Commands
{
    public class UpdateRewardCommandHandler : IRequestHandler<UpdateRewardCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRewardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRewardCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateRewardDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RewardDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Reward = await _unitOfWork.Repository<Hrm.Domain.Reward>().Get(request.RewardDto.RewardId);

            if (Reward is null)
            {
                throw new NotFoundException(nameof(Reward), request.RewardDto.RewardId);
            }

            _mapper.Map(request.RewardDto, Reward);

            await _unitOfWork.Repository<Hrm.Domain.Reward>().Update(Reward);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
