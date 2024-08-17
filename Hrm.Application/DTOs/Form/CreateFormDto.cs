using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form
{
    public class CreateFormDto
    {
        public int FormId { get; set; }
        public string FormName { get; set; }
        public string? Description { get; set; }
        public bool IsSystemForm { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
