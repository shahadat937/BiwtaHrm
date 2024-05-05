using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PostingOrderInfo.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.PostingOrderInfo.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Commands
{
    public class UpdatePostingOrderInfoCommandHandler : IRequestHandler<UpdatePostingOrderInfoCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePostingOrderInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePostingOrderInfoCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdatePostingOrderInfoDtoValidators();
            var validationResult = await validator.ValidateAsync(request.PostingOrderInfoDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var PostingOrderInfo = await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Get(request.PostingOrderInfoDto.PostingOrderInfoId);

            if (PostingOrderInfo is null)
            {
                throw new NotFoundException(nameof(PostingOrderInfo), request.PostingOrderInfoDto.PostingOrderInfoId);
            }

            _mapper.Map(request.PostingOrderInfoDto, PostingOrderInfo);

            await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Update(PostingOrderInfo);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
