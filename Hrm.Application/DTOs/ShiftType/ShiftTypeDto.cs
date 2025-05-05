using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ShiftType
{
    public class ShiftTypeDto : IShiftTypeDto
    {
        public int Id { get; set; }
        public string? ShiftName { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
