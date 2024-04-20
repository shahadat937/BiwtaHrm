using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankBranch.Validators;
using Hrm.Application.DTOs.BankBranch.ValidatorsBankBranch;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.BankBranch.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankBranch.Handlers.Commands
{
    public class UpdateBankBranchCommandHandler : IRequestHandler<UpdateBankBranchCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.BankBranch> _BankBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBankBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BankBranch> BankBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _BankBranchRepository = BankBranchRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateBankBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBankBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankBranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var BankBranch = await _unitOfWork.Repository<Hrm.Domain.BankBranch>().Get(request.BankBranchDto.BankBranchId);

            if (BankBranch is null)
            {
                throw new NotFoundException(nameof(BankBranch), request.BankBranchDto.BankBranchId);
            }

            var BankBranchName = request.BankBranchDto.BankBranchName.ToLower();

            IQueryable<Hrm.Domain.BankBranch> BankBranchs = _BankBranchRepository.Where(x => x.BankBranchName.ToLower() == BankBranchName);


            if (BankBranchs.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.BankBranchDto, BankBranch);

                await _unitOfWork.Repository<Hrm.Domain.BankBranch>().Update(BankBranch);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = BankBranch.BankBranchId;

            }
            return response;
        }
    }
}
