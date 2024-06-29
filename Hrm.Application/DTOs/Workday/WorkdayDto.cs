using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Workday
{
    public class WorkdayDto:IWorkdayDto
    {
        public int WorkdayId { get; set; }
        public int YearId { get; set; }
        public int WeekDayId { get; set; }
        public int MenuPosition {  get; set; }
        public string Remark { get; set; }
        public bool isActive { get; set; }
    }
}
