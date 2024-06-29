using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Workday
{
    public class CreateWorkdayDto : IWorkdayDto
    {
        public int WorkdayId { get; set; }
        public int YearId { get; set; }
        public int WeekendId { get; set; }
    }
}
