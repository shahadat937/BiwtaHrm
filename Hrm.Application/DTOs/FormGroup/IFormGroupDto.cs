using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormGroup
{
    public interface IFormGroupDto
    {
        public int FormGroupId { get; set; }
        public int ParentFieldId { get; set; }
        public int FormFieldId { get; set; }
    }
}
