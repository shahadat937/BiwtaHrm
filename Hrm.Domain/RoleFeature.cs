﻿using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class RoleFeature 
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

        //public virtual AspNetRoles Roles { get; set; } = null!;
        //public virtual Feature Features { get; set; } = null!;

    }
}
