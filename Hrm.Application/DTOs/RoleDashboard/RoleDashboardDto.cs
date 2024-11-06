using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RoleDashboard
{
    public class RoleDashboardDto : IRoleDashboardDto
    {
        public int Id { get; set; }
        public string? RoleId { get; set; }
        public bool? DashboardPermission { get; set; }
        public bool? EmpDashboardPermission { get; set; }
        public bool? IsActive { get; set; }

        public string? RoleName { get; set; }
    }
}
