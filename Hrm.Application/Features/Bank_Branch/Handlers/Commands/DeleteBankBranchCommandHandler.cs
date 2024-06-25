using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.BankBranch.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankBranch.Handlers.Commands
{
    public class DeleteBankBranchCommandHandler : IRequestHandler<DeleteBankBranchCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBankBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteBankBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var BankBranch = await _unitOfWork.Repository<Hrm.Domain.BankBranch>().Get(request.BankBranchId);

            if (BankBranch == null)
            {
                throw new NotFoundException(nameof(BankBranch), request.BankBranchId);
            }

            await _unitOfWork.Repository<Hrm.Domain.BankBranch>().Delete(BankBranch);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = BankBranch.BankBranchId;

            return response;
        }
    }
}
