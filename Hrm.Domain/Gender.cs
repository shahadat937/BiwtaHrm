using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class Gender : BaseDomainEntity
    {
        public int GenderId { get; set; }
        public string? GenderName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<EmpPersonalInfo>? EmpPersonalInfo { get; set; }
    }
}