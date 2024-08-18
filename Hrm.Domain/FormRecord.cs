using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class FormRecord : BaseDomainEntity
    {
        public int RecordId { get; set; }
        public int FormId { get; set; }
        public int EmpId { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public Form Form { get; set; }
        public EmpBasicInfo Employee {  get; set; }
        public virtual ICollection<FieldRecord> FieldRecords { get; } = new List<FieldRecord>();
    }
}
