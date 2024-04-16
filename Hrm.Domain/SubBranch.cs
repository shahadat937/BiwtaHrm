using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class SubBranch : BaseDomainEntity
    {
        public int SubBranchId { get; set; }
        public string? SubBranchName { get; set; }
        public int BranchId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}