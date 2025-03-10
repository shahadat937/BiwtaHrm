﻿using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TransferApproveInfo.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.TransferApproveInfo.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TransferApproveInfo.Handlers.Commands
{
    public class UpdateTransferApproveInfoCommandHandler : IRequestHandler<UpdateTransferApproveInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.TransferApproveInfo> _TransferApproveInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTransferApproveInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TransferApproveInfo> TransferApproveInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _TransferApproveInfoRepository = TransferApproveInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateTransferApproveInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateTransferApproveInfoDtoValidators();
            var validationResult = await validator.ValidateAsync(request.TransferApproveInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

           
                var TransferApproveInfo = await _unitOfWork.Repository<Hrm.Domain.TransferApproveInfo>().Get(request.TransferApproveInfoDto.TransferApproveInfoId);

                if (TransferApproveInfo is null)
                {
                    throw new NotFoundException(nameof(TransferApproveInfo), request.TransferApproveInfoDto.TransferApproveInfoId);
                }

                _mapper.Map(request.TransferApproveInfoDto, TransferApproveInfo);

                await _unitOfWork.Repository<Hrm.Domain.TransferApproveInfo>().Update(TransferApproveInfo);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = TransferApproveInfo.TransferApproveInfoId;



            return response;
        }
    }
}
