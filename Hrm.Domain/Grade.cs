using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Grade : BaseDomainEntity
    {
        public int GradeId { get; set; }
        public required string GradeName { get; set; }
        public int GradeTypeId { get; set; }
        public int GradeClassId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpJobDetail>? EmpJobDetail { get; set; }
        public virtual ICollection<EmpJobDetail>? FirstEmpJobDetail { get; set; }
        public virtual ICollection<EmpTransferPosting>? CurrentEmpTransferPosting { get; set; }
        public virtual ICollection<EmpTransferPosting>? UpdateEmpTransferPosting { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? CurrentEmpPromotionIncrement { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? UpdateEmpPromotionIncrement { get; set; }
        public virtual ICollection<Scale>? Scales { get; set; }

    }
}
