using Hrm.Application.DTOs.Reporting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Language.Requests.Queries
{
    public class GetEmpCountOnLanguageRequest : IRequest<EmpCountOnReportingDto>
    {
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
    }
}

