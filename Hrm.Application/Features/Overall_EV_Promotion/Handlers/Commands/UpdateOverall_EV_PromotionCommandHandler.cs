using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Overall_EV_Promotion.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Overall_EV_Promotion.Handlers.Commands
{
    public class UpdateOverall_EV_PromotionCommandHandler : IRequestHandler<UpdateOverall_EV_PromotionCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOverall_EV_PromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOverall_EV_PromotionCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateOverall_EV_PromotionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Overall_EV_PromotionDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Overall_EV_Promotion = await _unitOfWork.Repository<Hrm.Domain.Overall_EV_Promotion>().Get(request.Overall_EV_PromotionDto.Overall_EV_PromotionId);

            if (Overall_EV_Promotion is null)
            {
                throw new NotFoundException(nameof(Overall_EV_Promotion), request.Overall_EV_PromotionDto.Overall_EV_PromotionId);
            }

            _mapper.Map(request.Overall_EV_PromotionDto, Overall_EV_Promotion);

            await _unitOfWork.Repository<Hrm.Domain.Overall_EV_Promotion>().Update(Overall_EV_Promotion);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
