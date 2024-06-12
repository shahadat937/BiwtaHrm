using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpLanguageInfo : BaseDomainEntity
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? LanguageId { get; set; }
        public int? CompetenceId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        //public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        //public virtual Language? Language { get; set; }
        //public virtual Competence? Competence { get; set; }
    }
}
