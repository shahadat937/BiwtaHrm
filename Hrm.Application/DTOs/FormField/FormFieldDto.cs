using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormField
{
    public class FormFieldDto: IFormFieldDto
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string? Description { get; set; }
        public bool? IsRequired { get; set; }
        public bool? HasMultipleValue { get; set; }
        public bool? HasSelectable { get; set; }
        public int FieldTypeId { get; set; }
        public int? TotalSubquestion {  get; set; }
        public int? AssociateFieldId { get; set; }
        public string FieldTypeName { get; set; }
        public string HTMLTagName { get; set; }
        public string HTMLInputType { get; set; }
        public bool IsActive { get; set; }

        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
