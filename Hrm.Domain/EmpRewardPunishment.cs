using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpRewardPunishment : BaseDomainEntity
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? RewardPunishmentTypeId { get; set; }
        public int? RewardPunishmentPriorityId { get; set; }
        public DateOnly RewardPunishmentDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
        public string? OrderNo { get; set; }
        public bool? WithdrawStatus { get; set; }
        public DateOnly? WithdrawDate { get; set; }
        public int? OrderBy { get; set; }
        public int? ApplicationBy { get; set; }
        public int? ApproveById { get; set; }
        public DateOnly? ApproveDate { get; set; }
        public bool? ApproveStatus { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }

        // Navigation Properties
        public virtual EmpBasicInfo EmpBasicInfo { get; set; }
        public virtual RewardPunishmentType RewardPunishmentType { get; set; }
        public virtual RewardPunishmentPriority RewardPunishmentPriority { get; set; }
        public virtual EmpBasicInfo OrderByNavigation { get; set; }
        public virtual EmpBasicInfo ApplicationByNavigation { get; set; }
        public virtual EmpBasicInfo ApproveByNavigation { get; set; }
    }

}
