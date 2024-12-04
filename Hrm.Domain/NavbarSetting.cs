using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class NavbarSetting : BaseDomainEntity
    {
        public int Id { get; set; }
        public string? NavbarLogo { get; set; }
        public string? BrandLogo { get; set; }
        public string? BrandName { get; set; }
        public bool? ShowLogo { get; set; }
        public int? ThemId { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

        public virtual NavbarThem? NavbarThem { get; set; }
    }
}
