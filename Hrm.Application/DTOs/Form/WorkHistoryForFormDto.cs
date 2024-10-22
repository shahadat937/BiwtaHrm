using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form
{
    public class WorkHistoryForFormDto
    {
        public int EmpId { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName {  get; set; }
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
    }
}
