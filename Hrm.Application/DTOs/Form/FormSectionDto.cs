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
        public List<FormFieldValDto> Fields { get; set; } = new List<FormFieldValDto>();
    }
}
