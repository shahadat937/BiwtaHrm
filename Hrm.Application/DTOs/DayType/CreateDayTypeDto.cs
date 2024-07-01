using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DayType
{
    public class CreateDayTypeDto:IDayTypeDto
    {
        public int DayTypeId { get; set; }
        public string DayTypeName { get; set; }
        public string? Remark { get; set; }
    }
}
