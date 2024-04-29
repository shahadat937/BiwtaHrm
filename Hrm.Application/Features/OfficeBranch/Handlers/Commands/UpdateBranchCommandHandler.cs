using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Branch.Validators;
using Hrm.Application.DTOs.TrainingType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Branch.Requests.Commands;

using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeBranch.Handlers.Commands
{
    public class UpdateOfficeBranchCommandHandler : IRequestHandler<UpdateBranchCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.OfficeBranch> _OfficeBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOfficeBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.OfficeBranch> OfficeBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _OfficeBranchRepository = OfficeBranchRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var OfficeBranch = await _unitOfWork.Repository<Hrm.Domain.OfficeBranch>().Get(request.BranchDto.OfficeBranchId);

            if (OfficeBranch is null)
            {
                throw new NotFoundException(nameof(OfficeBranch), request.BranchDto.OfficeBranchId);
            }

            var OfficeBranchName = request.BranchDto.OfficeBranchName.ToLower();

            IQueryable<Hrm.Domain.OfficeBranch> OfficeBranchs = _OfficeBranchRepository.Where(x => x.OfficeBranchName.ToLower() == OfficeBranchName);


            if (OfficeBranchs.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.BranchDto.OfficeBranchName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.BranchDto, OfficeBranch);

                await _unitOfWork.Repository<Hrm.Domain.OfficeBranch>().Update(OfficeBranch);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = OfficeBranch.OfficeBranchId;

            }
            return response;
        }
    }
}
