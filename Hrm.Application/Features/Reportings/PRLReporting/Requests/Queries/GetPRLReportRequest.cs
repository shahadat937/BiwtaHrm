using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.VacancyReport.Requests.Queries
{
    public class GetPRLReportRequest : IRequest<object>
    {
        public QueryParams QueryParams { get; set; }
        public string? CurrentDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public bool IsPRL { get; set; }
        public bool IsRetirment { get; set; }
        public bool IsGone { get; set; }
        public bool IsWillGone { get; set; }
    }
}