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
       
        public int? EmpId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int? SubBranchId { get; set; }
        public int? SubDepartmentId { get; set; }
        public int? OfficeId { get; set; }
        public string? OfficeOrderNo { get; set; }
        public DateOnly? OfficeOrderDate { get; set; }
        public string? OrderOfficeBy { get; set; }
        public string? TransferSection { get; set; }
        public string? ReleaseType { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}