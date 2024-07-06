using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Upazila : BaseDomainEntity
    {
        public Upazila()
        {
            Thanas = new HashSet<Thana>();
        }

        public int UpazilaId { get; set; }
        public string UpazilaName { get; set; }
        public int? DistrictId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Thana> Thanas { get; set; }
        public virtual ICollection<EmpPresentAddress>? EmpPresentAddress { get; set; }
        public virtual ICollection<EmpPermanentAddress>? EmpPermanentAddress { get; set; }
    }
}
