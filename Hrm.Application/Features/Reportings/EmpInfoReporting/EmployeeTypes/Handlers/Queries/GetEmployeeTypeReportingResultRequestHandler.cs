using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Handlers.Queries
{
    public class GetEmployeeTypeReportingResultRequestHandler : IRequestHandler<GetEmployeeTypeReportingResultRequest, PagedResult<EmpReportingSearchResultDto>>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        public GetEmployeeTypeReportingResultRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<PagedResult<EmpReportingSearchResultDto>> Handle(GetEmployeeTypeReportingResultRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository.FilterWithInclude(x =>
                (request.Id == 0 || x.EmployeeTypeId == request.Id))
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Department)
                .ThenInclude(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Section)
                .ThenInclude(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Designation)
                        .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.EmpPersonalInfo)
                .Include(x => x.EmployeeType);

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
                    TypeName = x.EmployeeType.EmployeeTypeName?? "",
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
