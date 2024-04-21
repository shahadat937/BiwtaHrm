using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankAccountType.Validators;
using Hrm.Application.Features.BankAccountType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.BankAccountTypeBankAccountType.Validators;

namespace Hrm.Application.Features.BankAccountType.Handlers.Commands
{
    public class CreateBankAccountTypeCommandHandler : IRequestHandler<CreateBankAccountTypeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.BankAccountType> _bankAccountTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBankAccountTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BankAccountType> bankAccountTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bankAccountTypeRepository = bankAccountTypeRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBankAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankAccountTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankAccountTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var bankAccountTypeName = request.BankAccountTypeDto.BankAccountTypeName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.BankAccountType> bankAccountTypes = _bankAccountTypeRepository.Where(x => x.BankAccountTypeName.ToLower().Replace(" ", string.Empty) == bankAccountTypeName);


                if (BankAccountTypeNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.BankAccountTypeDto.BankAccountTypeName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

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
            }

            return response;
        }
        private bool BankAccountTypeNameExists(CreateBankAccountTypeCommand request)
        {
            var bankAccountTypeName = request.BankAccountTypeDto.BankAccountTypeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.BankAccountType> bankAccountTypes = _bankAccountTypeRepository.Where(x => x.BankAccountTypeName.Trim().ToLower().Replace(" ", string.Empty) == bankAccountTypeName);

            return bankAccountTypes.Any();
        }
    }
}
