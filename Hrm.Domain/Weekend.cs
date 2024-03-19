using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Weekend : BaseDomainEntity
    {
        public int WeekendId { get; set; }
        public string? WeekendName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}