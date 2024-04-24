using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class SubDepartment: BaseDomainEntity
    {
        public int SubDepartmentId { get; set; }
        public string? SubDepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public int MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public virtual Department Department { get; set; } = null!;
    }
}
