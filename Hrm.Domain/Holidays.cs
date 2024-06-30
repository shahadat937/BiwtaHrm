using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Holidays: BaseDomainEntity
    {
        public int HolidayId { get; set; }
        public int HolidayTypeId { get; set; }
        public int YearId { get; set; }
        public DateOnly HolidayStart {  get; set; }
        public DateOnly HolidayEnd { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

        public Year Year { get; set; }
        public HolidayType HolidayType { get; set; }

    }
}
