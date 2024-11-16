using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RoleFeatures
{
    public class FeaturePermissionDto
    {
        public bool? ViewStatus { get; set; }
        public bool? Add { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
        public bool? Report { get; set; }
    }
}
