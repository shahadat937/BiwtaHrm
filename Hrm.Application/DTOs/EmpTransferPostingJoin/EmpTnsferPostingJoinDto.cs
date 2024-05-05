using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTnsferPostingJoin
{
    public class EmpTnsferPostingJoinDto : IEmpTnsferPostingJoinDto
    {
        public int EmpTnsferPostingJoinId { get; set; }
        
        public int? EmpId { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public string? EmployeeName { get; set; }
    }
}
