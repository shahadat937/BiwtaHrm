﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OrderType
{
    public class SelectedOrderTypeDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Count { get; set; }
        public int? TotalCount { get; set; }
    }
}
