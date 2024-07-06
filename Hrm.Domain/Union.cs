using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Union : BaseDomainEntity
    {
        public int UnionId { get; set; } 
        public string? UnionName { get; set; }
        public int? ThanaId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpPresentAddress>? EmpPresentAddress { get; set; }
        public virtual ICollection<EmpPermanentAddress>? EmpPermanentAddress { get; set; }

    }
}
