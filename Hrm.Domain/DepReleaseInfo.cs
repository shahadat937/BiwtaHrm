using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class DepReleaseInfo : BaseDomainEntity
    {

        public int DepReleaseInfoId { get; set; }
        public int? EmpId { get; set; }     
        public string? OfficeOrderNo { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? OrderOfficeBy { get; set; }
        public string? ReferenceNo { get; set; }
        public string? DepClearance { get; set; }
        public string? ReleaseType { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}