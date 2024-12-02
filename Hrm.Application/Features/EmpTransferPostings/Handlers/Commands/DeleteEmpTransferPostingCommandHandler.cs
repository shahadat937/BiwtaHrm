using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmpTransferPostings.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Commands
{
    public class DeleteEmpTransferPostingCommandHandler : IRequestHandler<DeleteEmpTransferPostingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmpTransferPostingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpTransferPostingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var empTransferPosting = await _unitOfWork.Repository<EmpTransferPosting>().Get(request.Id);


            if (empTransferPosting == null)
            {
                throw new NotFoundException(nameof(empTransferPosting), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpTransferPosting>().Delete(empTransferPosting);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = empTransferPosting.Id;

            return response;

        }
    }
}
