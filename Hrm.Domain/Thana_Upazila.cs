using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Thana_Upazila : BaseDomainEntity
    {
        public int Thana_UpazilaId { get; set; } 
        public string? Thana_UpazilaName { get; set;}
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

    }
}
