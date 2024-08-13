using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AspNetUserRoles
{
    public class AspNetUserRolesDto : IAspNetUserRolesDto
    {
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
        public string? OldRoleId { get; set; }
        public string? NewRoleId { get; set; }
    }
}
