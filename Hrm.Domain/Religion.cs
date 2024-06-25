using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Religion : BaseDomainEntity
    {
        public int ReligionId { get; set; }
        public string? ReligionName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}