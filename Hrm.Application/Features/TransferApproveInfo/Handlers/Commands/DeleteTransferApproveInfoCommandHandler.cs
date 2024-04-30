using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.TransferApproveInfo.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TransferApproveInfo.Handlers.Commands
{
    public class DeleteTransferApproveInfoCommandHandler : IRequestHandler<DeleteTransferApproveInfoCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTransferApproveInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteTransferApproveInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var TransferApproveInfo = await _unitOfWork.Repository<Hrm.Domain.TransferApproveInfo>().Get(request.TransferApproveInfoId);

            if (TransferApproveInfo == null)
            {
                throw new NotFoundException(nameof(TransferApproveInfo), request.TransferApproveInfoId);
            }

            await _unitOfWork.Repository<Hrm.Domain.TransferApproveInfo>().Delete(TransferApproveInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = TransferApproveInfo.TransferApproveInfoId;

            return response;
        }
    }
}
