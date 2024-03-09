using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.BloodGroup.Handlers.Commands
{
    public class CreateBloodGroupCommandHandler : IRequestHandler<CreateBloodCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.BloodGroup> _bloodGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBloodGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BloodGroup> bloodGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bloodGroupRepository = bloodGroupRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBloodCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBloodGroupDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BloodGroupDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var bloodGroupName = request.BloodGroupDto.BloodGroupName.ToLower();

                IQueryable<Hrm.Domain.BloodGroup> bloodGroups = _bloodGroupRepository.Where(x => x.BloodGroupName.ToLower() == bloodGroupName);


                if (bloodGroups.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
                }
                else
                {
                    var BloodGroup = _mapper.Map<Hrm.Domain.BloodGroup>(request.BloodGroupDto);

                    BloodGroup = await _unitOfWork.Repository<Hrm.Domain.BloodGroup>().Add(BloodGroup);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = BloodGroup.BloodGroupId;
                }
            }

            return response;
        }

    }
}
