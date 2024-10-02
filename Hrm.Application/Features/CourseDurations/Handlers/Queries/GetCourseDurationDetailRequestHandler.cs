using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.CourseDurations.Requests.Queries;
using Hrm.Application.DTOs.CourseDuration;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.CourseDurations.Handlers.Queries
{
    public class GetCourseDurationDetailRequestHandler : IRequestHandler<GetCourseDurationDetailRequest, CourseDurationDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Domain.CourseDuration> _CourseDurationRepository;
        public GetCourseDurationDetailRequestHandler(IHrmRepository<Domain.CourseDuration> CourseDurationRepository, IMapper mapper)
        {
            _CourseDurationRepository = CourseDurationRepository;
            _mapper = mapper;
        }
        public async Task<CourseDurationDto> Handle(GetCourseDurationDetailRequest request, CancellationToken cancellationToken)
        {
            var CourseDuration = await _CourseDurationRepository.Get(request.CourseDurationId);
            return _mapper.Map<CourseDurationDto>(CourseDuration);
        }
    }
}
