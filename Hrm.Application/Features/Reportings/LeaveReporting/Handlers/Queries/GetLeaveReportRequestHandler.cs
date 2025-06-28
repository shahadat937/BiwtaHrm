using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveRequest;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.DTOs.Result;
using Hrm.Application.Features.Reportings.EmployeeList.Requests.Queries;
using Hrm.Application.Features.Reportings.VacancyReport.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.VacancyReport.Handlers.Queries
{
    public class GetLeaveReportRequestHandler : IRequestHandler<GetLeaveReportRequest, PagedResult<LeaveRequestDto>>
    {

        private readonly IHrmRepository<Hrm.Domain.LeaveRequest> _LeaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveReportRequestHandler(IHrmRepository<Hrm.Domain.LeaveRequest> LeaveRequestRepository, IMapper mapper)
        {
            _LeaveRequestRepository = LeaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<LeaveRequestDto>> Handle(GetLeaveReportRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.LeaveRequest> query = _LeaveRequestRepository.FilterWithInclude(x =>
                    (request.DepartmentId == 0 || x.Employee.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                    (request.SectionId == 0 || x.Employee.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId) &&
                    (request.DesignationId == 0 || x.Employee.EmpJobDetail.FirstOrDefault().Designation.DesignationSetupId == request.DesignationId) &&
                    (request.LeaveTypeId == 0 || x.LeaveTypeId == request.LeaveTypeId) &&
                    (request.FromDate <= x.ToDate && request.ToDate >= x.FromDate))
                    .Include(x => x.Employee)
                    .Include(x => x.Employee)
                        .ThenInclude(x => x.EmpJobDetail)
                            .ThenInclude(x => x.Department)
                    .Include(x => x.Employee)
                        .ThenInclude(x => x.EmpJobDetail)
                            .ThenInclude(x => x.Section)
                    .Include(x => x.Employee)
                        .ThenInclude(x => x.EmpJobDetail)
                            .ThenInclude(x => x.Designation)
                                .ThenInclude(x => x.DesignationSetup)
                    .Include(x => x.LeaveType)
                    .OrderBy(x => x.Employee.EmpJobDetail.FirstOrDefault().Department.DepartmentName)
                        .ThenBy(x => x.Employee.EmpJobDetail.FirstOrDefault().Section.SectionName)
                        .ThenBy(x => x.Employee.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name);

            var totalCount = await query.CountAsync(cancellationToken);

            query = query
                .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

            var resultData = _mapper.Map<List<LeaveRequestDto>>(query);

            var result = new PagedResult<LeaveRequestDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


            return result;
        }
    }
}
