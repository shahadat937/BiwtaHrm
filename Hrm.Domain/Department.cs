using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Department: BaseDomainEntity
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentNameBangla { get; set; }
        public string? DepartmentCode { get; set; }
        public int? OfficeId { get; set; }
        public int? UpperDepartmentId { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public int? Sequence { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public Office Office { get; set; }
        public Department UpperDepartment { get; set; }
        public ICollection<Department>? SubDepartments { get; set; }
        public ICollection<Designation>? Designations { get; set; }
        public ICollection<Section>? Section { get; set; }
        public virtual ICollection<EmpJobDetail>? EmpJobDetail { get; set; }
        public virtual ICollection<EmpJobDetail>? FirstEmpJobDetail { get; set; }
        public virtual ICollection<EmpTransferPosting>? CurrentEmpTransferPosting { get; set; }
        public virtual ICollection<EmpTransferPosting>? TransferEmpTransferPosting { get; set; }
        public virtual ICollection<EmpTransferPosting>? OrderOfficeTransfer { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? CurrentEmpPromotionIncrement { get; set; }
        //public virtual ICollection<EmpWorkHistory>? EmpWorkHistory { get; set; }
        public virtual ICollection<EmpOtherResponsibility>? EmpOtherResponsibility { get; set; }
        public virtual ICollection<EmpRewardPunishment>? EmpRewardPunishment { get; set; }
        public virtual ICollection<Notification>? Notification { get; set; }
        public virtual ICollection<OfficeOrder>? OfficeOrder { get; set; }

    }
}
