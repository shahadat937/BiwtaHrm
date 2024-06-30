using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class WeekDay : BaseDomainEntity
    {
        public int WeekDayId { get; set; }
        public string? WeekDayName { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Workday> Workday { get; } = new List<Workday>();
        public ICollection<Year> Year { get; } = new List<Year>();
    }
}