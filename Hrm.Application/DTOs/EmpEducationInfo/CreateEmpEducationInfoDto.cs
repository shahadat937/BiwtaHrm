using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpEducationInfo
{
    public class CreateEmpEducationInfoDto : IEmpEducationInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? ExamTypeId { get; set; }
        public int? BoardId { get; set; }
        public int? SubGroupId { get; set; }
        public int? CourseDurationId { get; set; }
        public int? ResultId { get; set; }
        public float? Point { get; set; }
        public int? PassingYear { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
