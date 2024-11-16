using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Section : BaseDomainEntity
    {
        public int SectionId { get; set; }
        public string? SectionName { get; set; }
        public string? SectionNameBangla { get; set; }
        public int? SectionCode { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? UpperSectionId { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public int? Sequence { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpJobDetail>? EmpJobDetail { get; set; }
        public virtual ICollection<EmpJobDetail>? FirstEmpJobDetail { get; set; }
        public Office Office { get; set; }
        public Department Department { get; set; }
        public Section UpperSection { get; set; }
        public ICollection<Section>? SubSections { get; set; }
        public ICollection<Designation>? Designations { get; set; }
        public virtual ICollection<EmpTransferPosting>? CurrentEmpTransferPosting { get; set; }
        public virtual ICollection<EmpTransferPosting>? TransferEmpTransferPosting { get; set; }
        public virtual ICollection<EmpWorkHistory>? EmpWorkHistory { get; set; }
        public virtual ICollection<EmpOtherResponsibility>? EmpOtherResponsibility { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? CurrentEmpPromotionIncrement { get; set; }
    }
}
