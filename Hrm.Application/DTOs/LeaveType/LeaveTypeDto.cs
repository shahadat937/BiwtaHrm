﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveType
{
    public class LeaveTypeDto: ILeaveTypeDto
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive {  get; set; }
        public bool ShowReport { get; set; }
        public bool? ELWorkDayCal {  get; set; }
        public string? Remark { get; set; }
        
    }
}
