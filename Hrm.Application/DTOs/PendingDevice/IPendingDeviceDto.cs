using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PendingDevice
{
    interface IPendingDeviceDto
    {
        public int Id { get; set; }
        public string SN {  get; set; }
    }
}
