﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance.Validators
{
    public class CreateBulkAttendanceDto
    {
        public IFormFile csvFile {  get; set; }
    }
}
