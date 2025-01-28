using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Gender.Requests.Queries
{
    public class GetGenderReportingResultRequest : IRequest<PagedResult<EmpReportingSearchResultDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? Id { get; set; }
        public bool? UnAssigned { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
    }
}
