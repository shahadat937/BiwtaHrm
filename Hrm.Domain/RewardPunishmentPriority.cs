using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class RewardPunishmentPriority : BaseDomainEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpRewardPunishment> EmpRewardPunishments { get; set; }
    }

}
