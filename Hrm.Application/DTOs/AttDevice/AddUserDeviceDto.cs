using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttDevice
{
    public class AddUserDeviceDto
    {
        public int? EmpId { get; set; }
        public string? IdCardNo { get; set; }
        public int DeviceId { get; set; }
        public string? Passwd { get; set; }
        public int GroupId { get; set; }
        public int Privilage { get; set; }
    }
}
