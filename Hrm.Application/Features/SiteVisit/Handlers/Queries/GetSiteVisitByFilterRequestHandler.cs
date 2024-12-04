using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SiteVisit;
using Hrm.Application.Features.SiteVisit.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Queries
{
    public class GetSiteVisitByFilterRequestHandler: IRequestHandler<GetSiteVisitByFilterRequest, List<SiteVisitDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.SiteVisit> _SiteVisitRepository;
        private readonly IMapper _mapper;

        public GetSiteVisitByFilterRequestHandler(IHrmRepository<Domain.SiteVisit> siteVisitRepository, IMapper mapper)
        {
            _SiteVisitRepository = siteVisitRepository;
            _mapper = mapper;
        }

        public async Task<List<SiteVisitDto>> Handle(GetSiteVisitByFilterRequest request, CancellationToken cancellationToken)
        {
            var siteVisits = _SiteVisitRepository.Where(x => true)
                .Include(e => e.Employees)
                .AsQueryable();

            if(request.filters.Status!=null&&request.filters.Status.Count()>0)
            {
                siteVisits = siteVisits.Where(x => request.filters.Status.Contains(x.Status));
            }

            if(request.filters.EmpId.HasValue)
            {
                siteVisits = siteVisits.Where(x=> x.EmpId == request.filters.EmpId);
            }

            var siteVisitList = await siteVisits.OrderByDescending(x=>x.SiteVisitId).ToListAsync();

            var siteVisitDtoList = _mapper.Map<List<SiteVisitDto>>(siteVisitList);

            return siteVisitDtoList;
            
        }
    }
}
