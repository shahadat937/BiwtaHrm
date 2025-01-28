using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class EmpReportingSearchResultDto
    {
        public string? IdCardNo { get; set; }
        public string? EmpName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? DesignationName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }
        public string? TypeName { get; set; }
    }
}
