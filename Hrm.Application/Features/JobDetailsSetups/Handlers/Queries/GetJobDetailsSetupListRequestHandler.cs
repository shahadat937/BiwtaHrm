using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.JobDetailsSetups.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.JobDetailsSetups.Handlers.Queries
{
    public class GetJobDetailsSetupListRequestHandler : IRequestHandler<GetJobDetailsSetupListRequest, object>
    {

        private readonly IHrmRepository<JobDetailsSetup> _JobDetailsSetupRepository;

        private readonly IMapper _mapper;

        public GetJobDetailsSetupListRequestHandler(IHrmRepository<JobDetailsSetup> JobDetailsSetupRepository, IMapper mapper)
        {
            _JobDetailsSetupRepository = JobDetailsSetupRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetJobDetailsSetupListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<JobDetailsSetup> JobDetailsSetups = _JobDetailsSetupRepository.Where(x => true);

            JobDetailsSetups = JobDetailsSetups.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var JobDetailsSetupsDtos = _mapper.Map<List<JobDetailsSetupDto>>(JobDetailsSetups);


            return JobDetailsSetupsDtos;
        }
    }
}
