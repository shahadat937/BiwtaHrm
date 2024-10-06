using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpEducationInfo : BaseDomainEntity
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
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

        public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        public virtual ExamType? ExamType { get; set; }
        public virtual Board? Board { get; set; }
        public virtual SubGroup? SubGroup { get; set; }
        public virtual CourseDuration? CourseDuration { get; set; }
        public virtual Result? Result { get; set; }
    }
}
