﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormRecord
{
    public class CreateFormRecordDto: IFormRecordDto
    {
        public int RecordId { get; set; }
        public int FormId { get; set; }
        public int EmpId { get; set; }
        public int? ReportingOfficerId { get; set; }
        public int? CounterSignatoryId {  get; set; }
        public int? ReceiverId { get; set; }
        public DateTime? ReportFrom { get; set; }
        public DateTime? ReportTo { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
