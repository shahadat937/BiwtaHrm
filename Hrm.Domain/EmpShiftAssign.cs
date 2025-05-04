using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpShiftAssign : BaseDomainEntity
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int ShiftId { get; set; }
        public bool IsActive { get; set; }

        public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        public virtual ShiftType? ShiftType { get; set; }
    }
}
