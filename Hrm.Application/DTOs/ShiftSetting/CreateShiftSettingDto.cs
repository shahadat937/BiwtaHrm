using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ShiftSetting
{
    public class CreateShiftSettingDto : IShiftSettingDto
    {
        public int Id { get; set; }
        public string? SettingName { get; set; }
        public int? ShiftTypeId { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? BufferTime { get; set; }
        public string? AbsentTime { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public string? Remark { get; set; }
    }
}
