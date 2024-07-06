using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class MaritalStatus : BaseDomainEntity
    {
        public int MaritalStatusId { get; set; }
        public string? MaritalStatusName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<EmpPersonalInfo>? EmpPersonalInfo { get; set; }
        public virtual ICollection<EmpChildInfo>? EmpChildInfo { get; set; }
    }
}
