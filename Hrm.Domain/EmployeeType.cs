using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmployeeType : BaseDomainEntity
    {
        public int EmployeeTypeId { get; set; }
        public string? EmployeeTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<EmpBasicInfo>? EmpBasicInfo { get; set; }

    }
}