using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Holidays
{
    public class HolidayDto: IHolidayDto
    {
        public int HolidayId { get; set; }
        public int HolidayTypeId { get; set; }
        public int YearId { get; set; }
        public int YearName { get; set; }
        public string HolidayTypeName { get; set; }
        public DateOnly HolidayStart { get; set; }
        public DateOnly HolidayEnd { get; set; }
        public int MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string Remark { get; set; }

    }
}
