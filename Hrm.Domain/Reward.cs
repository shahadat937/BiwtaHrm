using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class Reward : BaseDomainEntity
    {
        public int RewardId { get; set; }
        public string? RewardName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}