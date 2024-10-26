using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class LeaveFiles: BaseDomainEntity
    {
        public int Id { get; set; }
        public int? LeaveRequestId { get; set; }
        public string? FileTitle { get; set; }
        public string FilePath { get; set; }
        public string? Remark {  get; set; }

        public virtual LeaveRequest LeaveRequest { get; set; }
    }
}
