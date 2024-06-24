
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Role
{
    public class CreateRoleDto : IRoleDto
    {
       
        public string RoleName { get; set; } = null!;
        
    }
}
