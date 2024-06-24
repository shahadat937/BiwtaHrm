using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Religion.Validators;
using Hrm.Application.Features.Religion.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Religion.Validators;
using Hrm.Application.Features.Religion.Requests.Commands;

namespace Hrm.Application.Features.Religion.Handlers.Commands
{
    public class CreateReligionCommandHandler : IRequestHandler<CreateReligionCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Religion> _religionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateReligionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Religion> religionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _religionRepository = religionRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateReligionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateReligionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ReligionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                if (ReligionExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.ReligionDto.ReligionName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                }
                else
                {
                    var Religion = _mapper.Map<Hrm.Domain.Religion>(request.ReligionDto);

                    Religion = await _unitOfWork.Repository<Hrm.Domain.Religion>().Add(Religion);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Religion.ReligionId;
                }
                    
            }

            return response;
        }
        private bool ReligionExists(CreateReligionCommand request)
        {
            var normalizedReligionName = request.ReligionDto.ReligionName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Religion> matchingReligions = _religionRepository.Where(x => x.ReligionName.Trim().ToLower().Replace(" ", string.Empty) == normalizedReligionName);

            return matchingReligions.Any();
        }

    }
}
