using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Bank.Validators;
using Hrm.Application.Features.Bank.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.BankBank.Validators;

namespace hrm.Application.Features.Bank.Handlers.Commands
{
    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, BaseCommandResponse>
    {


        private readonly IHrmRepository<Hrm.Domain.Bank> _BankRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBankCommandHandler(IHrmRepository<Hrm.Domain.Bank> BankRepository, IUnitOfWork unitOfWork, IMapper mapper)
       
        {
            _BankRepository = BankRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _BankRepository = BankRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankDtoValidator ();
            var validatorResult = await validator.ValidateAsync(request.BankDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var BankName = request.BankDto.BankName.ToLower();

                IQueryable<Hrm.Domain.Bank> Banks = _BankRepository.Where(x => x.BankName.ToLower() == BankName);


                if (BankNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.BankDto.BankName}' already exists.";


                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

                }
                else
                {
                    var Bank = _mapper.Map<Hrm.Domain.Bank>(request.BankDto);

                    Bank = await _unitOfWork.Repository<Hrm.Domain.Bank>().Add(Bank);
                    await _unitOfWork.Save();
                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Bank.BankId;
                }
            }

            return response;
        }
        private bool BankNameExists(CreateBankCommand request)
        {

            var BankName = request.BankDto.BankName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Bank> Banks = _BankRepository.Where(x => x.BankName.Trim().ToLower().Replace(" ", string.Empty) == BankName);

            return Banks.Any();

        }
    }
}
