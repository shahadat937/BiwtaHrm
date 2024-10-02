using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.CourseDuration
{
    public interface ICourseDurationDto
    {
        public int Id { get; set; }
        public string Duration { get; set; }
        public int? Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
