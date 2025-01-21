using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormSection
{
    public class GetFormSectionDto : IFormSectionDto
    {
        public int FormSectionId { get; set; }
        public int SectionNo { get; set; }
        public int FormId { get; set; }
        public int? EmpId { get; set; }
        public string FormSectionName { get; set; }
        public string? SectionDescription { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
