using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Competence : BaseDomainEntity
    {
        public int CompetenceId { get; set; }
        public string CompetenceName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpLanguageInfo>? EmpLanguageInfo { get; set; }
    }
}
