using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Language.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Language.Handlers.Queries
{
    public class GetLanguageReportingResultRequestHandler : IRequestHandler<GetLanguageReportingResultRequest, PagedResult<EmpReportingSearchResultDto>>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        public GetLanguageReportingResultRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<PagedResult<EmpReportingSearchResultDto>> Handle(GetLanguageReportingResultRequest request, CancellationToken cancellationToken)
        {
            if (request.UnAssigned == false)
            {
                IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository
                   .FilterWithInclude(x =>
                       (request.Id == 0 || x.EmpLanguageInfo.Any(l => l.LanguageId == request.Id)) &&
                       (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                       (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                   .Include(x => x.EmpJobDetail)
                       .ThenInclude(x => x.Department)
                   .Include(x => x.EmpJobDetail)
                       .ThenInclude(x => x.Section)
                   .Include(x => x.EmpJobDetail)
                       .ThenInclude(x => x.Designation)
                       .ThenInclude(ds => ds.DesignationSetup)
                   .Include(x => x.EmpLanguageInfo) 
                       .ThenInclude(x => x.Language)
                   .Include(x => x.EmpPersonalInfo)
                   .OrderByDescending(x => x.EmpLanguageInfo.FirstOrDefault().LanguageId.HasValue)
                    .ThenBy(x => x.EmpLanguageInfo.FirstOrDefault().Language.LanguageName);

                var expandedQuery = query
                    .SelectMany(x => request.Id == 0
                    ? x.EmpLanguageInfo.DefaultIfEmpty() // No filter when request.Id == 0
                    : x.EmpLanguageInfo.Where(l => l.LanguageId == request.Id).DefaultIfEmpty(), (emp, lang) => new EmpReportingSearchResultDto
                    {
                        IdCardNo = emp.IdCardNo ?? "",
                        EmpName = (emp.FirstName + " " + emp.LastName) ?? "",
                        DepartmentName = emp.EmpJobDetail.FirstOrDefault().Department.DepartmentName ?? "",
                        SectionName = emp.EmpJobDetail.FirstOrDefault().Section.SectionName ?? "",
                        DesignationName = emp.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name ?? "",
                        TypeName = lang != null ? lang.Language.LanguageName : "", 
                        ContactNumber = emp.EmpPersonalInfo.FirstOrDefault().MobileNumber ?? "",
                        Email = emp.EmpPersonalInfo.FirstOrDefault().Email ?? "" ?? "",
                        Status = emp.EmpJobDetail.FirstOrDefault().ServiceStatus ?? false,
                    });

                var totalCount = await expandedQuery.CountAsync(cancellationToken);

                var resultData = await expandedQuery
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize)
                    .ToListAsync(cancellationToken);

                var result = new PagedResult<EmpReportingSearchResultDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);

                return result;

            }
            else
            {
                IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository.FilterWithInclude(x =>
                (x.EmpLanguageInfo.FirstOrDefault().LanguageId == null) &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Department)
                .ThenInclude(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Section)
                .ThenInclude(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Designation)
                        .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.EmpLanguageInfo)
                        .ThenInclude(x => x.Language)
                .OrderBy(x => x.EmpLanguageInfo.FirstOrDefault().Language.LanguageName);

                var totalCount = await query.CountAsync(cancellationToken);

                query = query
                    .OrderByDescending(x => x.Id)
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize);

                var resultData = await query
                    .Select(x => new EmpReportingSearchResultDto
                    {
                        IdCardNo = x.IdCardNo ?? "",
                        EmpName = (x.FirstName + " " + x.LastName) ?? "",
                        DepartmentName = x.EmpJobDetail.FirstOrDefault().Department.DepartmentName ?? "",
                        SectionName = x.EmpJobDetail.FirstOrDefault().Section.SectionName ?? "",
                        DesignationName = x.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name ?? "",
                        TypeName = x.EmpLanguageInfo.FirstOrDefault().Language.LanguageName ?? "",
                        ContactNumber = x.EmpPersonalInfo.FirstOrDefault().MobileNumber ?? "",
                        Email = x.EmpPersonalInfo.FirstOrDefault().Email ?? "",
                        Status = x.EmpJobDetail.FirstOrDefault().ServiceStatus ?? false,
                    })
                    .ToListAsync(cancellationToken);

                var result = new PagedResult<EmpReportingSearchResultDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


                return result;
            }
        }
    }
}
