﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpPromotionIncrement
{
    public class EmpPromotionIncrementDto : IEmpPromotionIncrementDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? CurrentDepartmentId { get; set; }
        public DateOnly? CurrentDeptJoinDate { get; set; }
        public int? CurrentSectionId { get; set; }
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
        public string? OrderNo { get; set; }
        public DateOnly? EffectiveDate { get; set; }
        public int? ApplicationById { get; set; }
        public bool? IsApproval { get; set; }
        public int? ApproveById { get; set; }
        public DateOnly? ApproveDate { get; set; }
        public bool? ApproveStatus { get; set; }
        public string? ApproveRemark { get; set; }
        public bool? ApplicationStatus { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public string? EmpIdCardNo { get; set; }
        public string? EmpName { get; set; }
        public string? CurrentDepartmentName { get; set; }
        public string? CurrentDesignationName { get; set; }
        public string? CurrentSectionName { get; set; }
        public string? CurrentGradeName { get; set; }
        public string? CurrentScaleName { get; set; }
        public string? UpdateDesignationName { get; set; }
        public string? UpdateGradeName { get; set; }
        public string? UpdateScaleName { get; set; }
        public string? ApproveByIdCardNo { get; set; }
        public string? ApproveByName { get; set; }
        public string? ApproveByDepartmentName { get; set; }
        public string? ApproveByDesignationName { get; set; }
        public int? ApplicationByDepartmentId { get; set; }
        public int? ApplicationBySectionId { get; set; }
        public int? ApplicationByDesignationId { get; set; }
        public int? ApplicationByResponsibityTypeId { get; set; }

        public int? ApproveByDepartmentId { get; set; }
        public int? ApproveBySectionId { get; set; }
        public int? ApproveByDesignationId { get; set; }
        public int? ApproveByResponsibityTypeId { get; set; }

    }
}
