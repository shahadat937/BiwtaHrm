using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmployeeList.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmployeeList.Handlers.Queries
{
    public class GetEmployeeListReportingRequestHandler : IRequestHandler<GetEmployeeListReportingRequest, PagedResult<EmployeeListReportingDto>>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;

        public GetEmployeeListReportingRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
        }

        public async Task<PagedResult<EmployeeListReportingDto>> Handle(GetEmployeeListReportingRequest request, CancellationToken cancellationToken)
        {
            var totalEmployee = await _EmpBasicInfoRepository.CountAsync(x => true);
            IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository.FilterWithInclude(x =>
                    (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                    (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                    .Include(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Department)
                    .Include(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Section)
                    .Include(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Designation)
                            .ThenInclude(ds => ds.DesignationSetup)
                    .Include(x => x.EmpPersonalInfo);

            IQueryable<EmpOtherResponsibility> empOtherResponsibility = _EmpOtherResponsibilityRepository.FilterWithInclude(x =>
                (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId) &&
                    (request.SectionId == 0 || x.SectionId == request.SectionId) && x.ServiceStatus == true)
                    .Include(x => x.Department)
                    .Include(x => x.Section)
                    .Include(x => x.Designation)
                        .ThenInclude(x => x.DesignationSetup)
                    .Include(x => x.EmpBasicInfo)
                    .Include(x => x.EmpBasicInfo)
                        .ThenInclude(x => x.EmpPersonalInfo);

            var totalCount = await query.CountAsync(cancellationToken) + await empOtherResponsibility.CountAsync(cancellationToken);



            var otherResponsibilityResultDate = await empOtherResponsibility
                .Select(x => new EmployeeListReportingDto
                {
                    DepartmentId = x.DepartmentId,
                    SectionId = x.SectionId,
                    DepartmentName = x.Department.DepartmentName ?? "",
                    SectionName = x.Section.SectionName ?? "",
                    DesignationName = x.ResponsibilityType != null && !string.IsNullOrEmpty(x.ResponsibilityType.Name)
                        ? $"{x.Designation.DesignationSetup.Name} ({x.ResponsibilityType.Name})"
                        : x.Designation.DesignationSetup.Name,
                    IdCardNo = x.EmpBasicInfo.IdCardNo ?? "",
                    EmpName = (x.EmpBasicInfo.FirstName + " " + x.EmpBasicInfo.LastName) ?? "",
                    Total = 0,
                    AllTotal = totalEmployee,
                    Mobile = x.EmpBasicInfo.EmpPersonalInfo.FirstOrDefault().MobileNumber ?? "",
                    JoinDate = x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().JoiningDate ?? null,
                    SectionSequence = x.Section.Sequence ?? 0,
                    DesignationSequence = x.Designation.MenuPosition ?? 0,
                })
                .ToListAsync(cancellationToken);

            var resultData = await query
                .Select(x => new EmployeeListReportingDto
                {
                    DepartmentId = x.EmpJobDetail.FirstOrDefault().Department.DepartmentId,
                    SectionId = x.EmpJobDetail.FirstOrDefault().Section.SectionId,
                    DepartmentName = x.EmpJobDetail.FirstOrDefault().Department.DepartmentName ?? "",
                    SectionName = x.EmpJobDetail.FirstOrDefault().Section.SectionName ?? "",
                    DesignationName = x.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name ?? "",
                    IdCardNo = x.IdCardNo ?? "",
                    EmpName = (x.FirstName + " " + x.LastName) ?? "",
                    Total = 0,
                    AllTotal = totalEmployee,
                    Mobile = x.EmpPersonalInfo.FirstOrDefault().MobileNumber ?? "",
                    JoinDate = x.EmpJobDetail.FirstOrDefault().JoiningDate ?? null,
                    SectionSequence = x.EmpJobDetail.FirstOrDefault().Section.Sequence ?? 0,
                    DesignationSequence = x.EmpJobDetail.FirstOrDefault().Designation.MenuPosition ?? 0
                })
                .ToListAsync(cancellationToken);

            var combinedResult = resultData
                .Concat(otherResponsibilityResultDate)
                .OrderByDescending(x => x.DepartmentId.HasValue)
                    .ThenBy(x => x.DepartmentId)
                        .ThenBy(x => x.SectionSequence)
                            .ThenBy(x => x.DesignationSequence)
                .ToList();

            var pagedResult = combinedResult
               .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
               .Take(request.QueryParams.PageSize)
               .ToList();

            foreach (var item in pagedResult)
            {
                if (item.SectionId != null) // If SectionId exists, count by Section
                {
                    item.Total = await _EmpBasicInfoRepository.CountAsync(b => b.EmpJobDetail.FirstOrDefault().SectionId == item.SectionId)+
                        await _EmpOtherResponsibilityRepository.CountAsync(x => x.SectionId == item.SectionId);
                }
                else // Otherwise, count by Department
                {
                    item.Total = await _EmpBasicInfoRepository.CountAsync(b => b.EmpJobDetail.FirstOrDefault().DepartmentId == item.DepartmentId && b.EmpJobDetail.FirstOrDefault().SectionId == null) + 
                        await _EmpOtherResponsibilityRepository.CountAsync(x => x.DepartmentId == item.DepartmentId && x.SectionId == null && x.ServiceStatus == true);
                }
            }


            var result = new PagedResult<EmployeeListReportingDto>(pagedResult, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


            return result;
        }
    }
}