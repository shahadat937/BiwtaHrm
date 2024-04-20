using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankBranch.Validators;
using Hrm.Application.DTOs.BankBranchBankBranch.Validators;
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
    public class CreateBankBranchCommandHandler : IRequestHandler<CreateBankBranchCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBankBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateBankBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankBranchDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }
            else
            {
                var BankBranch = _mapper.Map<Hrm.Domain.BankBranch>(request.BankBranchDto);
                BankBranch = await _unitOfWork.Repository<Hrm.Domain.BankBranch>().Add(BankBranch);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = BankBranch.BankBranchId;
            }
            return response;
        }
    }
}
