using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.WeekDay
{
    public interface IWeekDayDto
    {
        public int WeekDayId { get; set; }
        public string? WeekDayName { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
