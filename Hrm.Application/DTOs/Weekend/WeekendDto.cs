using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Weekend
{
    public class WeekendDto: IWeekendDto
    {
        public int WeekendId { get; set; }
        public string? WeekendName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
