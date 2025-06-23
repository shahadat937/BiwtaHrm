using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SiteSetting
{
    public interface ISiteSettingDto
    {
        public int Id { get; set; }
        public string? SiteName { get; set; }
        public string? SiteLogo { get; set; }
        public string? SiteTitle { get; set; }
        public string? DefaultPassword { get; set; }
        public string? FooterTitle { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
