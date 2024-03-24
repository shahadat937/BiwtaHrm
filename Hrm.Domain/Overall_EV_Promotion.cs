using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class Overall_EV_Promotion : BaseDomainEntity
    {
        public int Overall_EV_PromotionId { get; set; }
        public string? Overall_EV_PromotionName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}