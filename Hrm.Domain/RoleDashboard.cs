using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class RoleDashboard : BaseDomainEntity
    {
        public int Id { get; set; }
        public string? RoleId { get; set; }
        public bool? DashboardPermission { get; set; }
        public bool? EmpDashboardPermission { get; set; }
        public bool? IsActive { get; set; }

        public virtual AspNetRoles? AspNetRoles { get; set; }
    }
}
