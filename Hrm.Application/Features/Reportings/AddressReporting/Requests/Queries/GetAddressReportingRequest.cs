using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.AddressReporting.Requests.Queries
{
    public class GetAddressReportingRequest : IRequest<PagedResult<EmpAddressReportingResultDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? CountryId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? ThanaId { get; set; }
        public bool IsPresentAddress { get; set; }
    }
}
