using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class PostingOrderInfo : BaseDomainEntity
    {

        public int PostingOrderInfoId { get; set; }
        public string? PostingOrderInfoName { get; set; }
        public int? EmpId { get; set; }
        public string? OfficeOrderNo { get; set; }
        public DateTime? OfficeOrderDate { get; set; }
        public string? OrderOfficeBy { get; set; }
        public string? TransferSection { get; set; }
        public string? ReleaseType { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}