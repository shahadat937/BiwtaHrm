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
        public decimal? Result { get; set; }
        public string? CourseDuration { get; set; }
        public int? PassingYear { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        //public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        //public virtual ExamType? ExamType { get; set; }
        //public virtual Board? Board { get; set; }
        //public virtual SubGroup? SubGroup { get; set; }
    }
}
