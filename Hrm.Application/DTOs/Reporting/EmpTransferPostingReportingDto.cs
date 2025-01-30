using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class EmpTransferPostingReportingDto
    {
        public string? IdCardNo { get; set; }
        public string? EmpName { get; set; }
        public string? DepartmentFrom { get; set; }
        public string? DepartmentTo { get; set; }
        public string? PreviousDesignationName { get; set; }
        public string? CurrentDesignationName { get; set; }
        public string? PreviousSectionName { get; set; }
        public string? CurrentSectionName { get; set; }
        public string? OrderBy { get; set; }
        public DateOnly? OfficeOrderDate { get; set; }
        public DateOnly? DeptReleseDate { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }
    }
}
