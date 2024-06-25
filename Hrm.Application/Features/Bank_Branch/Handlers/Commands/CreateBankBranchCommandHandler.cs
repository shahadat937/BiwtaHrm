using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankBranch.Validators;
using Hrm.Application.Features.BankBranch.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.Features.BankBranch.Requests.Commands;

namespace Hrm.Application.Features.BankBranch.Handlers.Commands
{
    public class CreateBankBranchCommandHandler : IRequestHandler<CreateBankBranchCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.BankBranch> _BankBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBankBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BankBranch> BankBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _BankBranchRepository = BankBranchRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBankBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankBranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var BankBranchName = request.BankBranchDto.BankBranchName.ToLower();

                IQueryable<Hrm.Domain.BankBranch> BankBranchs = _BankBranchRepository.Where(x => x.BankBranchName.ToLower() == BankBranchName);


                if (BankBranchNameExists(request))
                {
                    response.Success = false;
                   // response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.BankBranchDto.BankBranchName}' already exists.";

                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
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
            }

            return response;
        }
        private bool BankBranchNameExists(CreateBankBranchCommand request)
        {
            var BankBranchName = request.BankBranchDto.BankBranchName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.BankBranch> BankBranchs = _BankBranchRepository.Where(x => x.BankBranchName.Trim().ToLower().Replace(" ", string.Empty) == BankBranchName);

            return BankBranchs.Any();
        }

    }
}
