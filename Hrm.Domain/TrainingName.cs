using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class TrainingName:BaseDomainEntity
    {
        public int TrainingNameId { get; set; }
        public string TrainingNames { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpPsiTrainingInfo>? EmpPsiTrainingInfo { get; set; }
        public virtual ICollection<EmpTrainingInfo>? EmpTrainingInfo { get; set; }
    }
}
