using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class DesignationSetup : BaseDomainEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameBangla { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
