using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Models.Identity
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[EmailAddress]
        public string? Email { get; set; }

        //[MinLength(6)]
        public string? UserName { get; set; }

        public string? PhoneNumber { get; set; }

        public int? EmpId { get; set; }

        public string? OldPassword { get; set; }

        public bool IsActive { get; set; }

        //[MinLength(6)]
        public string? Password { get; set; }
    }
}
