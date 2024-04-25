using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Bank.Validators;
using Hrm.Application.Features.Bank.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.BankBank.Validators;

namespace Hrm.Application.Features.Bank.Handlers.Commands
{
    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Bank> _bankRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Bank> bankRepository)
        {
            _BankRepository = BankRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bankRepository = bankRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var bankName = request.BankDto.BankName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Bank> banks = _bankRepository.Where(x => x.BankName.ToLower().Replace(" ", string.Empty) == bankName);


                if (BankNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.BankDto.BankName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

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
            }

            return response;
        }
        private bool BankNameExists(CreateBankCommand request)
        {
            var bankName = request.BankDto.BankName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Bank> banks = _bankRepository.Where(x => x.BankName.Trim().ToLower().Replace(" ", string.Empty) == bankName);

            return banks.Any();
        }
    }
}
