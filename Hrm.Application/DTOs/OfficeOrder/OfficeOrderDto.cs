using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OfficeOrder
{
    public class OfficeOrderDto : IOfficeOrderDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? OrderTypeId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public DateOnly? OrderDate { get; set; }
        public string? OrderNo { get; set; }
        public string? FileUrl { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
        public int? MenuPosition { get; set; }

        public string? OrderTypeName { get; set; }
        public string? OfficeName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? DesignationName { get; set; }
    }
}
