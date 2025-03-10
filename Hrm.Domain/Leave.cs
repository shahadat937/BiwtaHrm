﻿using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Leave : BaseDomainEntity
    {
        public int LeaveId { get; set; }
        public string? LeaveName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}