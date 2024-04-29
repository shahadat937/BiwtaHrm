using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Branch
{
    public class BranchDto: IBranchDto
    {
        public int OfficeBranchId { get; set; }
        public string? OfficeBranchName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
