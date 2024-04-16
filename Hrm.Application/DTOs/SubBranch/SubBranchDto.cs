using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubBranch
{
    public class SubBranchDto: ISubBranchDto
    {
      public int SubBranchId { get; set; }
        public string? SubBranchName { get; set; }
        public int BranchId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
