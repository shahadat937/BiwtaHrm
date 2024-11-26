using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PendingDevice
{
    public class PendingDeviceDto : IPendingDeviceDto
    {
        public int Id { get; set; }
        public string SN { get; set; }
        public string DeviceType { get; set; }
        public string DeviceIp { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
