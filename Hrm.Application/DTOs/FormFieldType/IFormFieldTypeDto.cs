using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormFieldType
{
    public interface IFormFieldTypeDto
    {
        public int FieldTypeId { get; set; }
        public string FieldTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
