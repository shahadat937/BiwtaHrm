using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.DTOs.Result;
using Hrm.Application.Features.Reportings.EmployeeList.Requests.Queries;
using Hrm.Application.Features.Reportings.VacancyReport.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.VacancyReport.Handlers.Queries
{
    public class GetVacancyReportRequestHandler : IRequestHandler<GetVacancyReportRequest, PagedResult<VacancyReportDto>>
    {

        private readonly IHrmRepository<Domain.Designation> _DesignationRepository;
        private readonly IHrmRepository<Domain.EmpJobDetail> _EmpJobDetailRepository;
        private readonly IHrmRepository<Domain.EmpOtherResponsibility> _EmpOtherResponsibilityRepository;

        public GetVacancyReportRequestHandler(IHrmRepository<Domain.Designation> DesignationRepository, IHrmRepository<EmpJobDetail> empJobDetailRepository, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository)
        {
            _DesignationRepository = DesignationRepository;
            _EmpJobDetailRepository = empJobDetailRepository;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
        }

        public async Task<PagedResult<VacancyReportDto>> Handle(GetVacancyReportRequest request, CancellationToken cancellationToken)
        {
            var totalPost = await _DesignationRepository.CountAsync(x => (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId) && (request.SectionId == 0 || x.SectionId == request.SectionId));

            var inService = await _EmpJobDetailRepository.CountAsync(x => x.ServiceStatus == true && x.DesignationId != null && (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId) && (request.SectionId == 0 || x.SectionId == request.SectionId)) + await _EmpOtherResponsibilityRepository.CountAsync(x => x.ServiceStatus == true && x.DesignationId != null && (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId) && (request.SectionId == 0 || x.SectionId == request.SectionId));

            var vacant = totalPost - inService;

            IQueryable<Domain.Designation> query = _DesignationRepository.FilterWithInclude(x =>
                 (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId) && (request.SectionId == 0 || x.SectionId == request.SectionId))
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.DesignationSetup);

            var totalCount = await query.GroupBy(d => new { d.DesignationSetupId, d.DepartmentId, d.SectionId }).CountAsync();


            var groupDesignation = query
                .GroupBy(d => new { d.DesignationSetupId, d.DepartmentId, d.SectionId })
                .Select(g => new VacancyDetailsDto
                {
                    DepartmentName = g.FirstOrDefault().Department.DepartmentName ?? "",
                    SectionName = g.FirstOrDefault().Section.SectionName ?? "",
                    DesignationName = g.FirstOrDefault().DesignationSetup.Name ?? "",
                    TotalPost = g.Count(),
                    TotalInService = g.FirstOrDefault().EmpJobDetail.Count(ejd => g.Any(d => d.DesignationId == ejd.DesignationId)) + g.FirstOrDefault().EmpOtherResponsibility.Count(or => g.Any(d => d.DesignationId == or.DesignationId && or.ServiceStatus == true)),
                    TotalVacantPost = g.Count() - (g.FirstOrDefault().EmpJobDetail.Count(ejd => g.Any(d => d.DesignationId == ejd.DesignationId)) + g.FirstOrDefault().EmpOtherResponsibility.Count(or => g.Any(d => d.DesignationId == or.DesignationId && or.ServiceStatus == true))),

                    DepartmentId = g.FirstOrDefault().Department.DepartmentId,
                    SectionSequence = g.FirstOrDefault().Section.Sequence,
                    DesignationPosition = g.FirstOrDefault().MenuPosition
                })
                .OrderBy(x => x.DepartmentId)
                    .ThenBy(x => x.SectionSequence)
                        .ThenBy(x => x.DesignationPosition)
                .ToList();

            var vacanceReport = new VacancyReportDto
            {
                TotalPost = totalPost,
                TotalInService = inService,
                TotalVacant = vacant,
                VacancyDetailsDto = groupDesignation.Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                        .Take(request.QueryParams.PageSize)
                .ToList()
            };

            var result = new PagedResult<VacancyReportDto>(new List<VacancyReportDto> { vacanceReport }, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);



            return result;

        }
    }
}
