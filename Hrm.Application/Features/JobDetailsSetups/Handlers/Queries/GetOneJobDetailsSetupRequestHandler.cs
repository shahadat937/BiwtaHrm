using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Features.JobDetailsSetups.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetup.Handlers.Queries
{
    public class GetOneJobDetailsSetupRequestHandler : IRequestHandler<GetOneJobDetailsSetupRequest, JobDetailsSetupDto>
    {

        private readonly IHrmRepository<Hrm.Domain.JobDetailsSetup> _JobDetailsSetupRepository;
        private readonly IMapper _mapper;
        public GetOneJobDetailsSetupRequestHandler(IHrmRepository<Hrm.Domain.JobDetailsSetup> JobDetailsSetupRepositoy, IMapper mapper)
        {
            _JobDetailsSetupRepository = JobDetailsSetupRepositoy;
            _mapper = mapper;
        }

        public async Task<JobDetailsSetupDto> Handle(GetOneJobDetailsSetupRequest request, CancellationToken cancellationToken)
        {
            var JobDetailsSetup = (await _JobDetailsSetupRepository.GetAll())
                .OrderByDescending(o => o.Id)
                .FirstOrDefault();
            return _mapper.Map<JobDetailsSetupDto>(JobDetailsSetup);
        }
    }
}