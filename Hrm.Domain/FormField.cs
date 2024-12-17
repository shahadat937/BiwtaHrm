using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class FormField : BaseDomainEntity
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
        public bool IsActive { get; set; }

        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public FormFieldType FieldType { get; set; }

        public virtual ICollection<FieldRecord> FieldRecords { get; } = new List<FieldRecord>();
        public virtual ICollection<FormSchema> FormSchemas { get; } = new List<FormSchema>();
        public virtual ICollection<SelectableOption> SelectableOptions { get; } = new List<SelectableOption>();

        public virtual ICollection<FormGroup> FormGroupParents { get; } = new List<FormGroup>();
        public virtual ICollection<FormGroup> FormGroupChild {  get; } = new List<FormGroup>();
    }
}
