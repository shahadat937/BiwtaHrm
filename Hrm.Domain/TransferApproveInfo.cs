using Hrm.Domain.Common;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class TransferApproveInfo : BaseDomainEntity
    {

        public int TransferApproveInfoId { get; set; }
        public int? PostingOrderInfoId { get; set; }
        public int? EmpId { get; set; }
        public bool? ApproveStatus { get; set; }
        public DateOnly? ApproveDate { get; set; }
        public int? ApproveBy { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}