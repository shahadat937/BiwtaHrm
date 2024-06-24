using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ExamType
{
    public interface IExamTypeDto
    {
        public int ExamTypeId { get; set; }
        public string? ExamTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
