using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Role
{
    public class RoleDto : IRoleDto
    {


        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string LoweredRoleName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
