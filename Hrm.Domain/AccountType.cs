using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class AccountType : BaseDomainEntity
    {
        public int AccountTypeId { get; set; }
        public string? AccoutType { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }


    }
}
