using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AspNetUserRoles
{
    public interface IAspNetUserRolesDto
    {
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
    }
}
