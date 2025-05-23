﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.GradeClass
{
    public class GradeClassDto : IGradeClassDto
    {
        public int GradeClassId { get; set; }
        public string GradeClassName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
