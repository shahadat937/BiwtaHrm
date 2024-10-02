using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class CancelledWeekend: BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime CancelDate { get; set; }
        public int? CancelledBy { get; set; }
        public bool IsActive { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }

        public virtual EmpBasicInfo? Employee {  get; set; }
    }
}
