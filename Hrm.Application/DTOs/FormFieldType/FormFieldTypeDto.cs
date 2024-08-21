using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormFieldType
{
    public class FormFieldTypeDto: IFormFieldTypeDto
    {
        public int FieldTypeId { get; set; }
        public string FieldTypeName { get; set; }
        public string? HTMLTagName { get; set; }
        public string? HTMLInputType { get; set; }
        public string? ValidationRegex { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
