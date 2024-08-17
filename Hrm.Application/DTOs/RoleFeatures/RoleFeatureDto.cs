using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RoleFeatures
{
    public class RoleFeatureDto
    {
        public int? FeatureKey { get; set; }
        public int? RoleFeatureId { get; set; }
        public string? RoleId { get; set; }
        public string? FeatureName { get; set; }
        public bool ViewStatus { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }

}
