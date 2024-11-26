using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class AttDeviceCommands: BaseDomainEntity
    {
        public int Id { get; set; }
        public string Command { get; set; }
        public string SN { get; set; }
    }
}
