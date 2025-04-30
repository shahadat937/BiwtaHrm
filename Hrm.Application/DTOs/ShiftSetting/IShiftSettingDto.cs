using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ShiftSetting
{
    public interface IShiftSettingDto
    {
        public int Id { get; set; }
        public string? SettingName { get; set; }
        public int? ShiftTypeId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeOnly? BufferTime { get; set; }
        public TimeOnly? AbsentTime { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public string? Remark { get; set; }
    }
}
