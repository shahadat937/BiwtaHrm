using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Relation : BaseDomainEntity
    {
        public int RelationId { get; set; }
        public string? RelationName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<EmpNomineeInfo>? EmpNomineeInfo { get; set; }
    }
}