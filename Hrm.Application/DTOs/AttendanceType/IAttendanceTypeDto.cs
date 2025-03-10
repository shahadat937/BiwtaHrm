﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttendanceType
{
    public interface IAttendanceTypeDto
    {
        public int AttendanceTypeId { get; set; }
        public string AttendanceTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
