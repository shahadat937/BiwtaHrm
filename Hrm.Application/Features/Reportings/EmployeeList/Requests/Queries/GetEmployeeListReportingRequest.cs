using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmployeeList.Requests.Queries
{
    public class GetEmployeeListReportingRequest : IRequest<object>
    {
        public QueryParams QueryParams { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
    }
}