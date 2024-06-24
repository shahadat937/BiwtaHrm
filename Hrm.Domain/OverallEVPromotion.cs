using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class OverallEVPromotion : BaseDomainEntity
    {
        public int OverallEVPromotionId { get; set; }
        public string? OverallEVPromotionName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}