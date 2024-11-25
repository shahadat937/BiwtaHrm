﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpFingerPrint
{
    public class EmpFingerPrintDto : IEmpFingerPrintDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? RightThumb { get; set; }
        public string? RightIndex { get; set; }
        public string? RightMiddle { get; set; }
        public string? RightRing { get; set; }
        public string? RightLittle { get; set; }
        public string? LeftThumb { get; set; }
        public string? LeftIndex { get; set; }
        public string? LeftMiddle { get; set; }
        public string? LeftRing { get; set; }
        public string? LeftLittle { get; set; }
        public bool IsActive { get; set; }
    }
}
