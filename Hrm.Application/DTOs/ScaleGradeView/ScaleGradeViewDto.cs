using Hrm.Application.DTOs.ScaleGradeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ScaleGradeView
{

    public class ScaleGradeViewDto 
    {
        public int ScaleId { get; set; }
        public string? ScaleName { get; set; }
        public int BasicPay { get; set; }
        public int GradeId { get; set; }
        public string? GradeName { get; set;}
        public bool IsActive { get; set; }

    }
}
