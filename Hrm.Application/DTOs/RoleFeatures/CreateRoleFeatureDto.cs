using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RoleFeatures
{
    public class CreateRoleFeatureDto
    {
        public int RoleFeatureId { get; set; }
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int FeatureKey { get; set; }
        public string? FeatureName { get; set; }
        public string? FeaturePath { get; set; }
        public bool? ViewStatus { get; set; }
        public bool? Add { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
        public bool? Report { get; set; }
    }
}
