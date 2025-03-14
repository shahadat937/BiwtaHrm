﻿using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class HairColor : BaseDomainEntity
    {
        public int HairColorId { get; set; }
        public string? HairColorName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<EmpPersonalInfo>? EmpPersonalInfo { get; set; }
    }
}