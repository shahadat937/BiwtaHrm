﻿using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class NavbarThem : BaseDomainEntity
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
        public bool IsActive { get; set; }

        public virtual ICollection<NavbarSetting>? NavbarSettings { get; set; }
    }

}
