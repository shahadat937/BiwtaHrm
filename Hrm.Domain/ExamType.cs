using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class ExamType : BaseDomainEntity
    {
        public int ExamTypeId { get; set; }
        public string? ExamTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpEducationInfo>? EmpEducationInfo { get; set; }
    }
}