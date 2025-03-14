﻿using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class BankAccountType : BaseDomainEntity
    {
        public int BankAccountTypeId { get; set; }
        public string BankAccountTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpBankInfo>? EmpBankInfo { get; set; }
    }
}
