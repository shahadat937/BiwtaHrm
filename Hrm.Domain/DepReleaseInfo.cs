﻿using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class DepReleaseInfo : BaseDomainEntity
    {

        public int DepReleaseInfoId { get; set; }
        public int? TransferApproveInfoId { get; set; }
        public int? PostingOrderInfoId { get; set; }
        public string? ApproveBy { get; set; }
        public bool? ApproveStatus { get; set; }
        public int? EmpId { get; set; }     
        public string? OfficeOrderNo { get; set; }
        public DateTime? ReleaseDate { get; set; } = DateTime.Now;
        public string? OrderOfficeBy { get; set; }
        public string? ReferenceNo { get; set; }
        public string? DepClearance { get; set; }
        public string? ReleaseType { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}