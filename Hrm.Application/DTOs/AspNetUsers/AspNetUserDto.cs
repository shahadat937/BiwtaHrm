﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AspNetUsers
{
    public class AspNetUserDto : IAspNetUserDto
    {

        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string? PasswordHash { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int? EmpId { get; set; }
        public bool IsActive { get; set; }
        public bool? CanEditProfile { get; set; }

        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? DesignationName { get; set; }
    }
}
