using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.GradeType
{
    public class CreateGradeTypeDto : IGradeTypeDto
    {
        public int GradeTypeId { get; set; }
        public required string GradeTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
