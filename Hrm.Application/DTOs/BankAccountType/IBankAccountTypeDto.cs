using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankAccountType
{
    public interface IBankAccountTypeDto
    {
        public int BankAccountTypeId { get; set; }
        public string? BankAccountTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
