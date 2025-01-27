using Hrm.Application.DTOs.Reporting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries
{
    public class GetEmpCountOnEmployeeTypeRequest : IRequest<EmpCountOnReportingDto>
    {
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
    }
}
