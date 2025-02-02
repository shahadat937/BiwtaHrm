using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.TransferPostingReporting.Requests.Queries
{
    public class GetTransferPostingResultRequest : IRequest<PagedResult<EmpTransferPostingReportingDto>>
    {
        public QueryParams QueryParams  { get; set; }
        public int? DepartmentFrom { get; set; }
        public int? DepartmentTo { get; set; }
        public int? SectionFrom { get; set; }
        public int? SectionTo { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
    }
}
