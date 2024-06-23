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
        public decimal? Result { get; set; }
        public string? CourseDuration { get; set; }
        public int? PassingYear { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
