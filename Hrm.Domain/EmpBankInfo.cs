using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpBankInfo : BaseDomainEntity
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public int? AccountTypeId { get; set; }
        public int? BankId { get; set; }
        public int? BranchId { get; set; }
        public string? RoutingNo { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        public virtual BankAccountType? AccountType { get; set; }
        public virtual Bank? Bank { get; set; }
        public virtual BankBranch? BankBranch { get; set; }
    }
}
