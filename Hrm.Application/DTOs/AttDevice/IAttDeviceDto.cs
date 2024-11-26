using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttDevice
{
    public interface IAttDeviceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SN { get; set; }
        public string? DeviceName { get; set; }
        public string? MAC { get; set; }
    }
}
