using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Gender : BaseDomainEntity
    {
        public int GenderId { get; set; }
        public string? GenderName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}