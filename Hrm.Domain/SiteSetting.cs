using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class SiteSetting : BaseDomainEntity
    {
        public int Id { get; set; }
        public string? SiteName { get; set; }
        public string? SiteLogo { get; set; }
        public string? FooterTitle { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
