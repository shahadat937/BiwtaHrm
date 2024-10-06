using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpEducationInfo
{
    public class EmpEducationInfoDto : IEmpEducationInfoDto
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

        public string? ExamTypeName { get; set; }
        public string? BoardName { get; set; }
        public string? SubGroupName { get; set; }
        public string? CourseDuration { get; set; }
        public string? ResultName { get; set; }
    }
}
