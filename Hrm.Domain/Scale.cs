using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Scale : BaseDomainEntity
    {
        public int ScaleId { get; set; }
        public string? ScaleName { get; set; }
        public int BasicPay { get; set; }
        public int GradeId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpJobDetail>? EmpJobDetail { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? CurrentEmpPromotionIncrement { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? UpdateEmpPromotionIncrement { get; set; }

    }
}
