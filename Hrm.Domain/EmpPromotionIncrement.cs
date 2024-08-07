using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpPromotionIncrement : BaseDomainEntity
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? CurrentDepartmentId { get; set; }
        public int? CurrentDesignationId { get; set; }
        public int? CurrentGradeId { get; set; }
        public int? CurrentScaleId { get; set; }
        public int? CurrentBasicPay { get; set; }
        public int? UpdateDesignationId { get; set; }
        public int? UpdateGradeId { get; set; }
        public int? UpdateScaleId { get; set; }
        public int? UpdateBasicPay { get; set; }
        public string? PromotionIncrementType { get; set; }
        public int? OrderById { get; set; }
        public DateOnly? OrderDate { get; set; }
        public DateOnly? EffectiveDate { get; set; }
        public int? ApplicationById { get; set; }
        public int? ApproveById { get; set; }
        public DateOnly? ApproveDate { get; set; }
        public bool? ApproveStatus { get; set; }
        public string? ApproveRemark { get; set; }
        public bool? ApplicationStatus { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        public virtual EmpBasicInfo? ApplicationBy { get; set; }
        public virtual EmpBasicInfo? OrderBy { get; set; }
        public virtual EmpBasicInfo? ApproveBy { get; set; }
        public virtual Department? CurrentDepartment { get; set; }
        public virtual Designation? CurrentDesignation { get; set; }
        public virtual Grade? CurrentGrade { get; set; }
        public virtual Scale? CurrentScale { get; set; }
        public virtual Designation? UpdateDesignation { get; set; }
        public virtual Grade? UpdateGrade { get; set; }
        public virtual Scale? UpdateScale { get; set; }
    }
}
