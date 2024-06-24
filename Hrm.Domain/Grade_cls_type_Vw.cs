using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Grade_cls_type_Vw
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
