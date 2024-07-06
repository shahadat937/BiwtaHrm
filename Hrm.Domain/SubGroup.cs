using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class SubGroup : BaseDomainEntity
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpEducationInfo>? EmpEducationInfo { get; set; }
    }
}