using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form
{
    public class FormSectionDto
    {
        public int SectionId { get; set; }
        public string SectionName {  get; set; }
        public int? EmpId { get; set; } // EmpId is required for his signature, if null it is not assigned yet
        public string? SignaturePhotoUrl { get; set; }
        public List<FormFieldValDto> Fields { get; set; } = new List<FormFieldValDto>();
    }
}
