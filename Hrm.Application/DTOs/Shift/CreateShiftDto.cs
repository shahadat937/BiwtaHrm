using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Shift
{
    public  class CreateShiftDto:IShiftDto
    {
        public int ShiftId { get; set; }
        public string? ShiftName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
