using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class FormGroup: BaseDomainEntity
    {
        public int FormGroupId { get; set; }
        public int ParentFieldId { get; set; }
        public int FormFieldId { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }
        public string? Remark {  get; set; }
        public int? MenuPosition { get; set; }

        public virtual FormField ParentField { get; set; }
        public virtual FormField ChildField { get; set; }
    }
}
