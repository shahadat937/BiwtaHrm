using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.NavbarSetting
{
    public class NavbarSettingDto : INavbarSettingDto
    {
        public int Id { get; set; }
        public string? NavbarLogo { get; set; }
        public string? BrandName { get; set; }
        public bool? ShowLogo { get; set; }
        public int? ThemId { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

        public string? ThemName { get; set; }
    }
}
