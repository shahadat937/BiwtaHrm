using Hrm.Application.DTOs.Grade_cls_type_Vw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Grade_cls_type_Vw
{
 
    public class CreateGrade_cls_type_VwDto : IGrade_cls_type_VwDto
    {
        public int GradeId { get; set; }
        public string? GradeName { get; set; }
        public int GradeClassId { get; set; }
        public string? GradeClassName { get; set; }
        public int GradeTypeId { get; set; }
        public string? GradeTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
