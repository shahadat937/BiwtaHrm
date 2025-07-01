using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.LeaveRequest;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.VacancyReport.Requests.Queries
{
    public class GetLeaveReportRequest : IRequest<PagedResult<LeaveRequestDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public int? LeaveTypeId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}