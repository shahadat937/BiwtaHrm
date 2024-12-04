using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class TrainingType:BaseDomainEntity
    {
        public int TrainingTypeId { get; set; }
        public string TrainingTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<EmpTrainingInfo>? EmpTrainingInfo { get; set; }
    }
}
