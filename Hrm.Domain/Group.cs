using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class Group : BaseDomainEntity
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}