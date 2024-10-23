using Hrm.Application.DTOs.FormField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormGroup
{
    public class CreateFormGroupDto: IFormGroupDto
    {
        public int FormGroupId { get; set; }
        public int ParentFieldId { get; set; }
        public int FormFieldId { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }
    }
}
