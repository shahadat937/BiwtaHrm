using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class OfficeBranch : BaseDomainEntity
    {
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? BranchNameBangla { get; set; }
        public string? BranchCode { get; set; }
        public int? OfficeId { get; set; }
        public int? UpperBranchId { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public int? Sequence { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}