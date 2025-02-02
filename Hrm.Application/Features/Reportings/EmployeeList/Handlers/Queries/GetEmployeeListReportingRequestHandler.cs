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

        public GetEmployeeListReportingRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
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
                    .Include(x => x.EmpPersonalInfo)
                    .OrderByDescending(x => x.EmpJobDetail.FirstOrDefault().DepartmentId.HasValue) 
                        .ThenBy(x => x.EmpJobDetail.FirstOrDefault().DepartmentId)
                            .ThenBy(x => x.EmpJobDetail.FirstOrDefault().Section.Sequence)
                                .ThenBy(x => x.EmpJobDetail.FirstOrDefault().Designation.MenuPosition);



            var totalCount = await query.CountAsync(cancellationToken);

            query = query
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize);


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
                })
                .ToListAsync(cancellationToken);

            foreach (var item in resultData)
            {
                if (item.SectionId != null) // If SectionId exists, count by Section
                {
                    item.Total = await _EmpBasicInfoRepository.CountAsync(b => b.EmpJobDetail.FirstOrDefault().SectionId == item.SectionId);
                }
                else // Otherwise, count by Department
                {
                    item.Total = await _EmpBasicInfoRepository.CountAsync(b => b.EmpJobDetail.FirstOrDefault().DepartmentId == item.DepartmentId && b.EmpJobDetail.FirstOrDefault().SectionId == null);
                }
            }


            var result = new PagedResult<EmployeeListReportingDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


            return result;
        }
    }
}