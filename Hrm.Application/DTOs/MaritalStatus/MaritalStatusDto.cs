﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.MaritalStatus
{
    public class MaritalStatusDto : IMaritalStatusDto
    {
        public int MaritalStatusId { get; set; }
        public required string MaritalStatusName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
