using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class ChildStatus : BaseDomainEntity
    {
        public int ChildStatusId { get; set; }
        public string? ChildStatusName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}