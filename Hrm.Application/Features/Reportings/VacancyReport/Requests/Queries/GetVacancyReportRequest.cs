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
    public class GetVacancyReportRequest : IRequest<PagedResult<VacancyReportDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
    }
}