using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Bank.Validators;
using Hrm.Application.DTOs.BankBank.Validators;
using Hrm.Application.Features.Bank.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Bank.Handlers.Commands
{
    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }
            else
            {
                var Bank = _mapper.Map<Hrm.Domain.Bank>(request.BankDto);
                Bank = await _unitOfWork.Repository<Hrm.Domain.Bank>().Add(Bank);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Bank.BankId;
            }
            return response;
        }
    }
}
