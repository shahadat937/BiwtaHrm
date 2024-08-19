using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormField
{
    public interface IFormFieldDto
    {
        public string FieldName { get; set; }
        public int FieldTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
