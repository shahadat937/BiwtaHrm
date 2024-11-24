using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class PendingDevice: BaseDomainEntity
    {
        public int Id { get; set; }
        public string SN {  get; set; }
        public string? DeviceType { get; set; }
        public DateTime? ExpireTime { get; set; }
    }
}
