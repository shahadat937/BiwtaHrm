using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class DayType : BaseDomainEntity
    {
        public int DayTypeId { get; set; }
        public required string DayTypeName { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Attendance> Attendances { get; } = new List<Attendance>();
    }
}
