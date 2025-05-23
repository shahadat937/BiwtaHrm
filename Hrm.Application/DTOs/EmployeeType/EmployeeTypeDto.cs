﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmployeeType
{
    public class EmployeeTypeDto: IEmployeeTypeDto
    {
        public int EmployeeTypeId { get; set; }
        public string? EmployeeTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
