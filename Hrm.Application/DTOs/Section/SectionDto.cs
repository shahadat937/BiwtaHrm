using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Section
{
    public class SectionDto: ISectionDto
    {
        public int SectionId { get; set; }
        public string? SectionName { get; set; }
        public string? SectionNameBangla { get; set; }
        public int? SectionCode { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? UpperSectionId { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public int? Sequence { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public bool? ShowAllDesignation { get; set; }

        public string? OfficeName { get; set; }
        public string? DepartmentName { get; set; }
        public string? UpperSectionName { get; set; }
    }
}
