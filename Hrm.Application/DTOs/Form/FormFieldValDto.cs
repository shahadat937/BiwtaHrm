using Hrm.Application.DTOs.FormField;
using Hrm.Application.DTOs.SelectableOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form
{
    public class FormFieldValDto
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public int FieldTypeId { get; set; }
        public string FieldTypeName { get; set; }
        public string HTMLTagName { get; set; }
        public string HTMLInputType { get; set; }
        public bool HasMultipleValue { get; set; }
        public bool HasSelectable { get; set; }
        public int FieldRecordId { get; set; }
        public int? TotalSubquestion {  get; set; }
        public int? AssociateFieldId { get; set; }
        public string FieldValue { get; set; }
        public string? Remark { get; set; }
        public List<SelectableOptionDto>? Options { get; set; } = new List<SelectableOptionDto>();
        public List<FormFieldValDto>? ChildFields { get; set; } = new List<FormFieldValDto>();
    }
}
