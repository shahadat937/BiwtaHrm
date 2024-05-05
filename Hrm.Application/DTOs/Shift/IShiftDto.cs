using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Shift
{
    public interface IShiftDto
    {
        public int ShiftId { get; set; }
        public string? ShiftName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? BufferTime { get; set; }
        public string? AbsentTime { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
