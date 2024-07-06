using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Thana : BaseDomainEntity
    {
        public int ThanaId { get; set; } 
        public string? ThanaName { get; set;}
        public int? UpazilaId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual Upazila? Upazila { get; set; }
        public virtual ICollection<EmpPresentAddress>? EmpPresentAddress { get; set; }
        public virtual ICollection<EmpPermanentAddress>? EmpPermanentAddress { get; set; }

    }
}
