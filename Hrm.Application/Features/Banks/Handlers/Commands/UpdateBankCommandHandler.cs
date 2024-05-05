using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Bank.Validators;
using Hrm.Application.DTOs.Bank.ValidatorsBank;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Bank.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrm.Application.Features.Bank.Handlers.Commands
{
    public class UpdateBankCommandHandler : IRequestHandler<UpdateExamTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Bank> _bankRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UpdateBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Bank> BankRepository)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bankRepository = BankRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateExamTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBankDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            //var BankName = request.BankDto.BankName.ToLower();
            var BankName = request.BankDto.BankName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.Bank> Banks = _bankRepository.Where(x => x.BankName.ToLower() == BankName);
            if (Banks.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.BankDto.BankName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Bank = await _unitOfWork.Repository<Hrm.Domain.Bank>().Get(request.BankDto.BankId);

                if (Bank is null)
                {
                    throw new NotFoundException(nameof(Bank), request.BankDto.BankId);
                }

                _mapper.Map(request.BankDto, Bank);

                await _unitOfWork.Repository<Hrm.Domain.Bank>().Update(Bank);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Bank.BankId;

            }

            return response;
        }
    }
}
