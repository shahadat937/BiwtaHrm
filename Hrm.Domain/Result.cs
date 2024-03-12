using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Result: BaseDomainEntity
    {
        public int ResultId { get; set; }
        public string? ResultName { get; set; }
        public int MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
