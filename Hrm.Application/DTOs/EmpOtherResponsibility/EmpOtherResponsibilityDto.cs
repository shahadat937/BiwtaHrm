using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpOtherResponsibility
{
    public class EmpOtherResponsibilityDto : IEmpOtherResponsibilityDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? EmpOtherResponsibilityId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool ServiceStatus { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }

        public string? ResponsibilityName { get; set; }
        public string? OfficeName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? DesignationName { get; set; }
    }
}
