using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class CourseDuration : BaseDomainEntity
    {
        public int Id { get; set; }
        public string Duration { get; set; }
        public int? Remark { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpTrainingInfo>? EmpTrainingInfo { get; set; }

    }
}
