using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Features.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.Features;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Features.Handlers.Queries
{
    public class GetFeatureListRequestHandler : IRequestHandler<GetFeatureListRequest, object>
    {

        private readonly IHrmRepository<Feature> _FeatureRepository;

        private readonly IMapper _mapper;

        public GetFeatureListRequestHandler(IHrmRepository<Feature> FeatureRepository, IMapper mapper)
        {
            _FeatureRepository = FeatureRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetFeatureListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<Feature> Features = _FeatureRepository.Where(x => x.IsActive == true);

            Features = Features.OrderBy(x => x.OrderNo);

            var FeaturesDtos = _mapper.Map<List<FeatureDto>>(Features);


            return FeaturesDtos;
        }
    }
}
