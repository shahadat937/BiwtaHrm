using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Holidays
{
    public class CreateHolidayDto:IHolidayDto
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateOnly HolidayDate { get; set; }
        public int HolidayTypeId { get; set; }
        public int YearId { get; set; }
        public int? OfficeId { get; set; }
        public int? OfficeBranchId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
    }
}
