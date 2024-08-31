using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class FormSchema : BaseDomainEntity
    {
        public int SchemaId { get; set; }
        public int FormId { get; set; }
        public int FieldId { get; set; }
        public int? Section { get; set; }
        public bool IsActive { get; set; }
        public int? OrderNo { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public Form Form { get; set; }
        public FormField FormField { get; set; }
    }
}
