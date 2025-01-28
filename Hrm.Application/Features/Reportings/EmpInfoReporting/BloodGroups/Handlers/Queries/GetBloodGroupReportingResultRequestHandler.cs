using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Handlers.Queries
{
    public class GetBloodGroupReportingResultRequestHandler : IRequestHandler<GetBloodGroupReportingResultRequest, PagedResult<EmpReportingSearchResultDto>>
    {

        private readonly IHrmRepository<EmpPersonalInfo> _EmpPersonalInfoRepository;
        public GetBloodGroupReportingResultRequestHandler(IHrmRepository<EmpPersonalInfo> EmpPersonalInfoRepository)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<PagedResult<EmpReportingSearchResultDto>> Handle(GetBloodGroupReportingResultRequest request, CancellationToken cancellationToken)
        {

            IQueryable<EmpPersonalInfo> query = _EmpPersonalInfoRepository.FilterWithInclude(x =>
                (request.Id == 0 || x.BloodGroupId == request.Id))
                .Include(x => x.EmpBasicInfo)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Department)
                .Include(x => x.EmpBasicInfo)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Section)
                .Include(x => x.EmpBasicInfo)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Designation)
                            .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.BloodGroup);


            var totalCount = await query.CountAsync(cancellationToken);

            query = query
                .OrderByDescending(x => x.Id)
                .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

            var resultData = await query
                .Select(x => new EmpReportingSearchResultDto
                {
                    IdCardNo = x.EmpBasicInfo.IdCardNo ?? "",
                    EmpName = (x.EmpBasicInfo.FirstName + " " + x.EmpBasicInfo.LastName) ?? "",
                    DepartmentName = x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Department.DepartmentName ?? "",
                    SectionName = x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Section.SectionName ?? "",
                    DesignationName = x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name ?? "",
                    TypeName = x.BloodGroup.BloodGroupName ?? "",
                    ContactNumber = x.MobileNumber ?? "",
                    Email = x.Email ?? "",
                    Status = x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().ServiceStatus ?? false,
                })
                .ToListAsync(cancellationToken);




            var result = new PagedResult<EmpReportingSearchResultDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


            return result;
        }
    }
}