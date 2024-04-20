using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class BankBranch : BaseDomainEntity
    {
        public int BankBranchId { get; set; }
        public string BankBranchName { get; set; }
        public string BankBranchcode { get; set; }
        public string BankBranchAddress { get; set; }
        public string BankBranchContractNo { get; set; }
        public string BankBranchPerson { get; set; }
        public string Email { get; set; }
        public int NoOfEmployee { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
