using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankAccountType.Validators;
using Hrm.Application.DTOs.BankAccountTypeBankAccountType.Validators;
using Hrm.Application.Features.BankAccountType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankAccountType.Handlers.Commands
{
    public class CreateBankAccountTypeCommandHandler : IRequestHandler<CreateBankAccountTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBankAccountTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateBankAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankAccountTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankAccountTypeDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }
            else
            {
                var BankAccountType = _mapper.Map<Hrm.Domain.BankAccountType>(request.BankAccountTypeDto);
                BankAccountType = await _unitOfWork.Repository<Hrm.Domain.BankAccountType>().Add(BankAccountType);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = BankAccountType.BankAccountTypeId;
            }
            return response;
        }
    }
}
