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

        private readonly IHrmRepository<Hrm.Domain.BankAccountType> _BankAccountTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBankAccountTypeCommandHandler(IHrmRepository<Hrm.Domain.BankAccountType> BankAccountTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _BankAccountTypeRepository = BankAccountTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBankAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankAccountTypeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.BankAccountTypeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var BankAccountTypeName = request.BankAccountTypeDto.BankAccountTypeName.ToLower();

                IQueryable<Hrm.Domain.BankAccountType> BankAccountTypes = _BankAccountTypeRepository.Where(x => x.BankAccountTypeName.ToLower() == BankAccountTypeName);

                if (BankAccountTypeNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.BankAccountTypeDto.BankAccountTypeName}' already exists.";

                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    var BankAccountType = _mapper.Map<Hrm.Domain.BankAccountType>(request.BankAccountTypeDto);

                    BankAccountType = await _unitOfWork.Repository<Hrm.Domain.BankAccountType>().Add(BankAccountType);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = BankAccountType.BankAccountTypeId;
                }
            }
            return response;
        }
        private bool BankAccountTypeNameExists(CreateBankAccountTypeCommand request)
        {
            var BankAccountTypeName = request.BankAccountTypeDto.BankAccountTypeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.BankAccountType> BankAccountTypes = _BankAccountTypeRepository.Where(x => x.BankAccountTypeName.Trim().ToLower().Replace(" ", string.Empty) == BankAccountTypeName);

            return BankAccountTypes.Any();
        }
    }
}
