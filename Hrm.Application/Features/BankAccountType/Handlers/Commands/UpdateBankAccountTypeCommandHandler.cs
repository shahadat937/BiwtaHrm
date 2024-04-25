using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankAccountType.Validators;
using Hrm.Application.DTOs.BankAccountType.ValidatorsBankAccountType;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.BankAccountType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankAccountType.Handlers.Commands
{
    public class UpdateBankAccountTypeCommandHandler : IRequestHandler<UpdateBankAccountTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.BankAccountType> _bankAccountTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBankAccountTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BankAccountType> bankAccountTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bankAccountTypeRepository = bankAccountTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateBankAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBankAccountTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankAccountTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var BankAccountType = await _unitOfWork.Repository<Hrm.Domain.BankAccountType>().Get(request.BankAccountTypeDto.BankAccountTypeId);

            if (BankAccountType is null)
            {
                throw new NotFoundException(nameof(BankAccountType), request.BankAccountTypeDto.BankAccountTypeId);
            }

            var bankAccountTypeName = request.BankAccountTypeDto.BankAccountTypeName.ToLower();

            IQueryable<Hrm.Domain.BankAccountType> bankAccountTypes = _bankAccountTypeRepository.Where(x => x.BankAccountTypeName.ToLower() == bankAccountTypeName);


            if (bankAccountTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.BankAccountTypeDto.BankAccountTypeName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.BankAccountTypeDto, BankAccountType);

                await _unitOfWork.Repository<Hrm.Domain.BankAccountType>().Update(BankAccountType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = BankAccountType.BankAccountTypeId;

            }
            return response;
        }
    }
}
