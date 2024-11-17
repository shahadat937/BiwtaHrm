using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.NavbarSetting
{
    public interface INavbarSettingDto
    {
        public int Id { get; set; }
        public string? NavbarLogo { get; set; }
        public string? BrandLogo { get; set; }
        public string? BrandName { get; set; }
        public bool? ShowLogo { get; set; }
        public int? ThemId { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
