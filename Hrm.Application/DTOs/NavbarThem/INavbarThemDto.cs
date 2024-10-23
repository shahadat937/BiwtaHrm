using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.NavbarThem
{
    public interface INavbarThemDto
    {
        public int Id { get; set; }
        public string? ThemName { get; set; }
        public string? BgColor { get; set; }
        public string? BrandBg { get; set; }
        public string? TogglerBg { get; set; }
        public string? TogglerHoverBg { get; set; }
        public string? LinkColor { get; set; }
        public string? LinkActiveColor { get; set; }
        public string? LinkActiveBg { get; set; }
        public string? LinkHoverColor { get; set; }
        public string? LinkHoverBg { get; set; }
        public string? LinkIconColor { get; set; }
        public string? LinkIconHoverColor { get; set; }
        public string? GroupBg { get; set; }
        public string? GroupToggleColor { get; set; }
        public int? Width { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
    }
}
