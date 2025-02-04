﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankAccountType.Validators;
using Hrm.Application.DTOs.BankAccountType.ValidatorsBankAccountType;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.BankAccountType.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
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


        public UpdateBankAccountTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BankAccountType> BankAccountTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bankAccountTypeRepository = BankAccountTypeRepository;
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


            //var BankAccountTypeName = request.BankAccountTypeDto.BankAccountTypeName.ToLower();
            var BankAccountTypeName = request.BankAccountTypeDto.BankAccountTypeName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.BankAccountType> BankAccountTypes = _bankAccountTypeRepository.Where(x => x.BankAccountTypeName.ToLower() == BankAccountTypeName);



            if (BankAccountTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.BankAccountTypeDto.BankAccountTypeName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";

                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var BankAccountType = await _unitOfWork.Repository<Hrm.Domain.BankAccountType>().Get(request.BankAccountTypeDto.BankAccountTypeId);

                if (BankAccountType is null)
                {
                    throw new NotFoundException(nameof(BankAccountType), request.BankAccountTypeDto.BankAccountTypeId);
                }

                _mapper.Map(request.BankAccountTypeDto, BankAccountType);

                await _unitOfWork.Repository<Hrm.Domain.BankAccountType>().Update(BankAccountType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = BankAccountType.BankAccountTypeId;

            }

            return response;
        }
    }
}
