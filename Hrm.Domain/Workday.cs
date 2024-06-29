using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Workday : BaseDomainEntity
    {
        public int WorkdayId { get; set; }
        public int YearId { get; set; }
        public int WeekDayId { get; set; }
        public string? Remark {  get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public virtual Year year { get; set; } = null!;
        public virtual WeekDay weekDay { get; set; } = null!;


    }
}
