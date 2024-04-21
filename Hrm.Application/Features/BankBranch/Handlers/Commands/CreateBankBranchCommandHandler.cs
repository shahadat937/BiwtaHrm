using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankBranch.Validators;
using Hrm.Application.Features.BankBranch.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.BankBranchBankBranch.Validators;

namespace Hrm.Application.Features.BankBranch.Handlers.Commands
{
    public class CreateBankBranchCommandHandler : IRequestHandler<CreateBankBranchCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.BankBranch> _bankBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBankBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BankBranch> bankBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bankBranchRepository = bankBranchRepository;
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
                //   var bankBranchName = request.BankBranchDto.BankBranchName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.BankBranch> bankBranchs = _bankBranchRepository.Where(x => x.BankBranchName.ToLower().Replace(" ", string.Empty) == bankBranchName);


                if (BankBranchNameExists(request))
                {
                    response.Success = false;
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
            var bankBranchName = request.BankBranchDto.BankBranchName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.BankBranch> bankBranchs = _bankBranchRepository.Where(x => x.BankBranchName.Trim().ToLower().Replace(" ", string.Empty) == bankBranchName);

            return bankBranchs.Any();
        }
    }
}
