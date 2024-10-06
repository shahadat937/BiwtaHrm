using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RoleFeatures
{
    public class ModuleFeatureDto
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
        public IconComponentDto IconComponent { get; set; }
        public List<ModuleFeaturesGroupDto> Children { get; set; }
    }

    public class ModuleFeaturesGroupDto
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
        public int? OrderNo { get; set; }
        public bool? IsActive { get; set; }
    }
    public class IconComponentDto
    {
        public string? Name { get; set; }
    }
}
