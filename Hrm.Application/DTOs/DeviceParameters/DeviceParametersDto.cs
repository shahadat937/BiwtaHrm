using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DeviceParameters
{
    public class DeviceParametersDto: IDeviceParametersDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
