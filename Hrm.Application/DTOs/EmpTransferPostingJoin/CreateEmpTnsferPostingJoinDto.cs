using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTnsferPostingJoin
{
    public class CreateEmpTnsferPostingJoinDto : IEmpTnsferPostingJoinDto
    {
        public int EmpTnsferPostingJoinId { get; set; }
        public int? DepReleaseInfoId { get; set; }
        public int? PostingOrderInfoId { get; set; }
        public string? ApproveBy { get; set; }
        public bool? ApproveStatus { get; set; }
        public int? EmpId { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }


    }
}
