using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.CancelledWeekend
{
    public class CancelledWeekendDto: ICancelledWeekend
    {
        public int Id { get; set; }
        public DateTime CancelDate { get; set; }
        public int? CancelledBy { get; set; }
        public string? CancelledByEmpFirstName { get; set; }
        public string? CancelledByEmpLastName { get; set; }
        public bool IsActive { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }
    }
}
