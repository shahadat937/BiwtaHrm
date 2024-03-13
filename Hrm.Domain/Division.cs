using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Division : BaseDomainEntity
    {
        public int DivisionId { get; set; }
        public string? DivisionName { get; set; }
        public int? CountryId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}