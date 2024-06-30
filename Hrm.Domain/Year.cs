using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Year : BaseDomainEntity
    {
        public int YearId { get; set; }
        public int YearName { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Workday> Workday { get; } = new List<Workday>();
        public ICollection<WeekDay> weekDays { get; } = new List<WeekDay>();
        public ICollection<Holidays> Holidays { get; } = new List<Holidays>();
    }
}
