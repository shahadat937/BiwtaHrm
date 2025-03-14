﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Designation
{
    public class DesignationDto : IDesignationDto
    {
        public int DesignationId { get; set; }
        public int? DesignationSetupId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? DesignationName { get; set; }
        public string? OfficeName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? EmployeeName { get; set; }
    }
}
