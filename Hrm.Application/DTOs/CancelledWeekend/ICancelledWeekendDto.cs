using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.CancelledWeekend
{
    public interface ICancelledWeekend
    {
        public DateTime CancelDate { get; set; }
        public bool IsActive { get; set; }
    }
}
