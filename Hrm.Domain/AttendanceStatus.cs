using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class AttendanceStatus:BaseDomainEntity
    {
        public int AttendanceStatusId { get; set; }
        public string AttendanceStatusName { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }
    }
}
