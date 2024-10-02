using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.CourseDurations.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.CourseDuration.Handlers.Commands
{
    public class CreateCourseDurationCommandHandler : IRequestHandler<CreateCourseDurationCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.CourseDuration> _CourseDurationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCourseDurationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.CourseDuration> CourseDurationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _CourseDurationRepository = CourseDurationRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateCourseDurationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            if (request.CourseDurationDto.Name == null)
            {
                response.Success = false;
                response.Message = "Creation Failed! CourseDuration Name is Requires";
            }
            else
            {
                if (CourseDurationNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.CourseDurationDto.Name}' already exists.";
                    
                }
                else
                {
                    var CourseDuration = _mapper.Map<Hrm.Domain.CourseDuration>(request.CourseDurationDto);

                    CourseDuration = await _unitOfWork.Repository<Hrm.Domain.CourseDuration>().Add(CourseDuration);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = CourseDuration.Id;
                }
            }

            return response;
        }
        private bool CourseDurationNameExists(CreateCourseDurationCommand request)
        {

            IQueryable<Domain.CourseDuration> CourseDurations = _CourseDurationRepository.Where(x => x.Name == request.CourseDurationDto.Name);

             return CourseDurations.Any();
        }
    }
}
