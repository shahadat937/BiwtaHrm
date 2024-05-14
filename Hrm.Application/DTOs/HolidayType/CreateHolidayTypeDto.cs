using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HolidayType
{
    public  class CreateHolidayTypeDto:IHolidayTypeDto
    {
        public int HolidayTypeId { get; set; }
        public string? HolidayTypeName { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
