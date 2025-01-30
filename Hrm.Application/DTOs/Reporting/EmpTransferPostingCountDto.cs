using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class EmpTransferPostingCountDto
    {
        public int? TotalApplication { get; set; }
        public int? TotalApproved { get; set; }
        public int? TotalDepartmentPending { get; set; }
        public int? TotalDepartmentApprove{ get; set; }
        public int? TotalDepartmentReject {get; set; }
        public int? JoingingPending { get; set; }
        public int? JoingingApproved { get; set; }
        public int? JoingRejected { get; set; }
    }

}
