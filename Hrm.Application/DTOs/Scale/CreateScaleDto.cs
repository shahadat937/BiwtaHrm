using Hrm.Application.DTOs.Scale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Scale
{
 
    public class CreateScaleDto : IScaleDto
    {
        public int ScaleId { get; set; }
        public string? ScaleName { get; set; }
        public int BasicPay { get; set; }
        public int GradeId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? GradeName { get; set; }
    }
}
