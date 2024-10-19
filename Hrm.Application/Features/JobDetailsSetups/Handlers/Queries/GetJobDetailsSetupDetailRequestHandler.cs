using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Features.JobDetailsSetups.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.JobDetailsSetups.Handlers.Queries
{
    public class GetJobDetailsSetupsDetailRequestHandler : IRequestHandler<GetJobDetailsSetupDetailRequest, JobDetailsSetupDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<JobDetailsSetup> _JobDetailsSetupRepository;
        public GetJobDetailsSetupsDetailRequestHandler(IHrmRepository<JobDetailsSetup> JobDetailsSetupRepository, IMapper mapper)
        {
            _JobDetailsSetupRepository = JobDetailsSetupRepository;
            _mapper = mapper;
        }
        public async Task<JobDetailsSetupDto> Handle(GetJobDetailsSetupDetailRequest request, CancellationToken cancellationToken)
        {
            var JobDetailsSetup = await _JobDetailsSetupRepository.Get(request.Id);
            return _mapper.Map<JobDetailsSetupDto>(JobDetailsSetup);
        }
    }
}
