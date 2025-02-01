using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class EmployeeListReportingDto
    {
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        public int? Total { get; set; }
        public int? AllTotal { get; set; }
        public int? EmpId { get; set; }
        public string? IdCardNo { get; set; }
        public string? EmpName { get; set; }
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public string? Mobile { get; set; }
        public DateOnly? JoinDate { get; set; }
    }
}
