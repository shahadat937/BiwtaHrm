using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Devicecmd
{
    public class DeviceCmdResponse
    {
        public string? Id { get; set; }
        public string? Return { get; set; }
        public string? CMD { get; set; }
        public string? DeviceName { get; set; }
        public string? MAC { get; set; }
        public string? IpAddress { get; set; }
    }
}
