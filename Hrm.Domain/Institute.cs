using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Institute : BaseDomainEntity
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<EmpTrainingInfo>? EmpTrainingInfo { get; set; }
    }
}
