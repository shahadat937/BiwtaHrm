using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class EmpAddressReportingResultDto
    {
        public string? IdCardNo { get; set; }
        public string? EmpName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? DesignationName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }
        public string? CountryName { get; set; }
        public string? DivisionName { get; set; }
        public string? DistricName { get; set; }
        public string? ThanaName { get; set; }
    }
}
