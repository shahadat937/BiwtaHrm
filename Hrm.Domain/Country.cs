using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Country : BaseDomainEntity
    {
      
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

       // public virtual ICollection<Division> Divisions { get; set; }
    }
}