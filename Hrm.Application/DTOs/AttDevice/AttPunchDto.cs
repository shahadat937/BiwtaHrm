using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttDevice
{
    public class AttPunchDto
    {
        public string IdCardNo { get; set; }
        public DateOnly PunchDate { get; set; }
        public TimeOnly PunchTime { get; set; }
        public int PunchState { get; set; }
        public int PunchType { get; set; }
    }
}
