using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Branch.Validators;
using Hrm.Application.DTOs.TrainingType.Validators;
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
    public class CreateOfficeBranchCommandHandler : IRequestHandler<CreateBranchCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.OfficeBranch> _OfficeBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOfficeBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.OfficeBranch> OfficeBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _OfficeBranchRepository = OfficeBranchRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var OfficeBranchName = request.OfficeBranchDto.OfficeBranchName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.OfficeBranch> OfficeBranchs = _OfficeBranchRepository.Where(x => x.OfficeBranchName.ToLower().Replace(" ", string.Empty) == OfficeBranchName);


                if (OfficeBranchNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.BranchDto.OfficeBranchName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var OfficeBranch = _mapper.Map<Hrm.Domain.OfficeBranch>(request.BranchDto);

                    OfficeBranch = await _unitOfWork.Repository<Hrm.Domain.OfficeBranch>().Add(OfficeBranch);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = OfficeBranch.OfficeBranchId;
                }
            }

            return response;
        }
        private bool OfficeBranchNameExists(CreateBranchCommand request)
        {
            var OfficeBranchName = request.BranchDto.OfficeBranchName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.OfficeBranch> OfficeBranchs = _OfficeBranchRepository.Where(x => x.OfficeBranchName.Trim().ToLower().Replace(" ", string.Empty) == OfficeBranchName);

            return OfficeBranchs.Any();
        }
    }
}
