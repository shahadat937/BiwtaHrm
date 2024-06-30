using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.DTOs.SiteVisit;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Application.Features.SiteVisit.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Queries
{
    public class GetSiteVisitRequestHandler:IRequestHandler< GetSiteVisitRequest , object>
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
            var SiteVisits = _SiteVisitRepository.Where(x => true)
                .Include(e => e.Employees)
                .ToList();

            //SiteVisits = SiteVisits.OrderByDescending(x => x.SiteVisitId);
            //var siteVisitDtos = SiteVisits.Select(sv => new SiteVisitDto
            //{
            //  SiteVisitId = sv.SiteVisitId,
            //EmpId = sv.EmpId,
            //EmpName = sv.Employees.EmpEngName,
            //FromDate = sv.FromDate,
            //ToDate = sv.ToDate,
            //VisitPlace = sv.VisitPlace,
            //VisitPurpose = sv.VisitPurpose,
            //Remark = sv.Remark
            //}).ToList();

            var siteVisitsDto = _mapper.Map<List<SiteVisitDto>>(SiteVisits);

            return siteVisitsDto;
        }
    }
}
