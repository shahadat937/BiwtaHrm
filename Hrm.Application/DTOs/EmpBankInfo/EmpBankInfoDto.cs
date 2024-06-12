using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpBankInfo
{
    public class EmpBankInfoDto : IEmpBankInfoDto
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
    }
}
