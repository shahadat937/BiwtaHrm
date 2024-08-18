using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RoleFeatures
{
    public class ModuleFeatureDto
    {
        public string? ModuleName { get; set; }
        public string? ModuleLink { get; set; }
        public List<ModuleFeaturesGroupDto> Features { get; set; }
    }

    public class ModuleFeaturesGroupDto
    {
        public string? RoleName { get; set; }
        public string? FeatureName { get; set; }
        public string? FeatureLink { get; set; }
        public bool ViewStatus { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
