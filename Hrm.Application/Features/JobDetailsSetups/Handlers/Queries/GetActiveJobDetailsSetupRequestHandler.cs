using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Features.JobDetailsSetups.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetups.Handlers.Queries
{
    public class GetActiveJobDetailsSetupRequestHandler : IRequestHandler<GetActiveJobDetailsSetupRequest, object>
    {

        private readonly IHrmRepository<JobDetailsSetup> _JobDetailsSetupRepository;

        private readonly IMapper _mapper;

        public GetActiveJobDetailsSetupRequestHandler(IHrmRepository<JobDetailsSetup> JobDetailsSetupRepository, IMapper mapper)
        {
            _JobDetailsSetupRepository = JobDetailsSetupRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetActiveJobDetailsSetupRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            var JobDetailsSetups = await _JobDetailsSetupRepository.FindOneAsync(x => x.IsActive == true);


            var JobDetailsSetupsDtos = _mapper.Map<JobDetailsSetupDto>(JobDetailsSetups);


            return JobDetailsSetupsDtos;
        }
    }
}
