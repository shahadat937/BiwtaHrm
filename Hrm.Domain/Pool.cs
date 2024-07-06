using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EyesColor : BaseDomainEntity
    {
        public int EyesColorId { get; set; }
        public string? EyesColorName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<EmpPersonalInfo>? EmpPersonalInfo { get; set; }
    }
}