using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.DTOs.SiteVisit;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Application.Features.SiteVisit.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Queries
{
    public class GetSiteVisitRequestHandler:IRequestHandler< GetSiteVisitRequest ,object>
    {
        private readonly IHrmRepository<Hrm.Domain.SiteVisit> _SiteVisitRepository;
        private readonly IMapper _mapper;

        public GetSiteVisitRequestHandler(IHrmRepository<Hrm.Domain.SiteVisit> SiteVisitRepository, IMapper mapper)
        {
            _SiteVisitRepository = SiteVisitRepository;
            _mapper = mapper;
            
        }

        public async Task<object> Handle(GetSiteVisitRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.SiteVisit> SiteVisits = _SiteVisitRepository.Where(x => true);

            SiteVisits = SiteVisits.OrderByDescending(x => x.SiteVisitId);

            var SiteVisitDtos = _mapper.Map<List<SiteVisitDto>>(SiteVisits);

            return SiteVisitDtos;
        }
    }
}
