﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class AspNetRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<RoleFeature>? RoleFeature { get; set; }
        public virtual ICollection<RoleDashboard>? RoleDashboard { get; set; }
    }
}
