using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttDevice
{
    public class AttDevicesDto: IAttDeviceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SN {  get; set; }
        public string? DeviceName { get; set; }
        public string? Area { get; set; }
        public string? MAC {  get; set; }
        public string? LocalIpAddress { get; set; }
        public string? OEM {  get; set; }
        public string? PushVersion { get; set; }
        public string? Language {  get; set; }
        public string Timezone {  get; set; }
        public bool AccDevices { get; set; }
        public DateTime? LastOnline { get; set; }
        public bool Status { get; set; }

    }
}
