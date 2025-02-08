using AutoMapper;
using AutoMapper.Internal;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
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

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        public GetBloodGroupReportingResultRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<PagedResult<EmpReportingSearchResultDto>> Handle(GetBloodGroupReportingResultRequest request, CancellationToken cancellationToken)
        {
            if (request.UnAssigned == false)
            {
                IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository.FilterWithInclude(x =>
                    (request.Id == 0 || x.EmpPersonalInfo.FirstOrDefault().BloodGroupId == request.Id) &&
                    (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                    (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                    .Include(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Department)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Section)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Designation)
                            .ThenInclude(ds => ds.DesignationSetup)
                    .Include(x => x.EmpPersonalInfo)
                    .Include(x => x.EmpPersonalInfo)
                        .ThenInclude(x => x.BloodGroup)
                    .OrderByDescending(x => x.EmpPersonalInfo.FirstOrDefault().BloodGroupId.HasValue)
                        .ThenBy(x => x.EmpPersonalInfo.FirstOrDefault().BloodGroup.BloodGroupName);

                var totalCount = await query.CountAsync(cancellationToken);

                query = query
                    //.OrderByDescending(x => x.EmpPersonalInfo.FirstOrDefault().BloodGroup.BloodGroupName)
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
                        TypeName = x.EmpPersonalInfo.FirstOrDefault().BloodGroup.BloodGroupName ?? "",
                        ContactNumber = x.EmpPersonalInfo.FirstOrDefault().MobileNumber ?? "",
                        Email = x.EmpPersonalInfo.FirstOrDefault().Email ?? "",
                        Status = x.EmpJobDetail.FirstOrDefault().ServiceStatus ?? false,
                    })
                    .ToListAsync(cancellationToken);

                var result = new PagedResult<EmpReportingSearchResultDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


                return result;
            }
            else
            {
                IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository.FilterWithInclude(x =>
                (x.EmpPersonalInfo.FirstOrDefault().BloodGroupId == null) &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Department)
                .ThenInclude(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Section)
                .ThenInclude(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Designation)
                        .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.EmpPersonalInfo)
                    .Include(x => x.EmpPersonalInfo)
                        .ThenInclude(x => x.BloodGroup);

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
                        TypeName = x.EmpPersonalInfo.FirstOrDefault().BloodGroup.BloodGroupName ?? "",
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
