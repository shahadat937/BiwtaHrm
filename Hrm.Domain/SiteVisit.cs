﻿using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class SiteVisit:BaseDomainEntity
    {
        public int SiteVisitId { get; set; }
        public int EmpId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string VisitPlace { get; set; }
        public string VisitPurpose { get; set; }
        public string? Remark { get; set; }
        public string? Status { get; set; }

        public EmpBasicInfo Employees { get; set; }
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
