using AutoMapper;
using Hrm.Application.Features.SiteVisit.Requests.Queries;
using Hrm.Application.DTOs.SiteVisit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;

namespace Hrm.Application.Features.SiteVisit.Handlers.Queries
{

    public class GetSiteVisitByIdRequestHandler : IRequestHandler<GetSiteVisitByIdRequest, SiteVisitDto>
    {

        private readonly IHrmRepository<Hrm.Domain.SiteVisit> _SiteVisitRepository;
        private readonly IMapper _mapper;

        public GetSiteVisitByIdRequestHandler(IHrmRepository<Domain.SiteVisit> siteVisitRepository, IMapper mapper)
        {
            _SiteVisitRepository = siteVisitRepository;
            _mapper = mapper;
        }

        public async Task<SiteVisitDto> Handle(GetSiteVisitByIdRequest request, CancellationToken cancellationToken)
        {
            var SiteVisit= await _SiteVisitRepository.Get(request.SiteVisitId);
            return _mapper.Map<SiteVisitDto>(SiteVisit);
        }
    }
}
