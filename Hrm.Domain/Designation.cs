using Hrm.Domain.Common;


namespace Hrm.Domain
{
    public class Designation: BaseDomainEntity
    {
        public int DesignationId { get; set; }
        public int? DesignationSetupId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public DesignationSetup DesignationSetup { get; set; }
        public Office Office { get; set; }
        public Department Department { get; set; }
        public Section Section { get; set; }
        public virtual ICollection<EmpJobDetail>? EmpJobDetail { get; set; }
        public virtual ICollection<EmpJobDetail>? FirstEmpJobDetail { get; set; }
        public virtual ICollection<EmpTransferPosting>? CurrentEmpTransferPosting { get; set; }
        public virtual ICollection<EmpTransferPosting>? TransferEmpTransferPosting { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? CurrentEmpPromotionIncrement { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? UpdateEmpPromotionIncrement { get; set; }
        //public virtual ICollection<EmpWorkHistory>? EmpWorkHistory { get; set; }
        public virtual ICollection<EmpOtherResponsibility>? EmpOtherResponsibility { get; set; }
        public virtual ICollection<EmpRewardPunishment>? EmpRewardPunishment { get; set; }
    }
}
