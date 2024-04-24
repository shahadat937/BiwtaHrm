using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Department: BaseDomainEntity
    {
        public Department()
        {
            
            SubDepartment = new HashSet<SubDepartment>();
        }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SubDepartment> SubDepartment { get; set; }

    }
}
