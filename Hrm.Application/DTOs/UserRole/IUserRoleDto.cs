using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.UserRole
{
    public interface IUserRoleDto
    {
        public int UserRoleId { get; set; }
        public string? UserRoleName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
