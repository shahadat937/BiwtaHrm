using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Ward : BaseDomainEntity
    {
        public int WardId { get; set; }
        public string WardName { get; set; }
        public int? UnionId { get; set; } 
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}