using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubBranch.Validators;
using Hrm.Application.Features.SubBranch.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.appraisalFormType.Validators;
using Hrm.Application.DTOs.SubBranchSubBranch.Validators;

namespace Hrm.Application.Features.SubBranch.Handlers.Commands
{
    public class CreateSubBranchCommandHandler : IRequestHandler<CreateSubBranchCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSubBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SubBranchRepository = SubBranchRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateSubBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSubBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SubBranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var SubBranchName = request.SubBranchDto.SubBranchName.ToLower();

                IQueryable<Hrm.Domain.SubBranch> SubBranchs = _SubBranchRepository.Where(x => x.SubBranchName.ToLower() == SubBranchName);


                if (SubBranchNameExists(request))
                {
                    response.Success = false;
                    // response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.SubBranchDto.SubBranchName}' already exists.";

                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var SubBranch = _mapper.Map<Hrm.Domain.SubBranch>(request.SubBranchDto);

                    SubBranch = await _unitOfWork.Repository<Hrm.Domain.SubBranch>().Add(SubBranch);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = SubBranch.SubBranchId;
                }
            }

            return response;
        }
        private bool SubBranchNameExists(CreateSubBranchCommand request)
        {
            var SubBranchName = request.SubBranchDto.SubBranchName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.SubBranch> SubBranchs = _SubBranchRepository.Where(x => x.SubBranchName.Trim().ToLower().Replace(" ", string.Empty) == SubBranchName);

            return SubBranchs.Any();
        }

    }
}
