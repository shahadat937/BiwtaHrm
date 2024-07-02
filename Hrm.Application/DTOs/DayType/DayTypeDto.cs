using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DayType
{
    public class DayTypeDto:IDayTypeDto
    {
        public int DayTypeId { get; set; }
        public string DayTypeName { get; set; }
        public bool IsActive { get; set; }
        public string? Remark {  get; set; }
    }
}
