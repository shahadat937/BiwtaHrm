﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpBasicInfo
{
    public class EmpBasicInfoDto : IEmpBasicInfoDto
    {
        public int Id { get; set; }
        public string? IdCardNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FirstNameBangla { get; set; }
        public string? LastNameBangla { get; set; }
        public string? AspNetUserId { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? NID { get; set; }
        public int? SmartNID { get; set; }
        public DateOnly? ProbabilityRetirementDate { get; set; }
        public DateOnly? PRLDate { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpGovNo { get; set; }
        public string? PersonalFileNo { get; set; }
        public int? EmployeeTypeId { get; set; }
        public bool? UserStatus { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public string? EmployeeTypeName { get; set; }
        public string? DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public string? SectionName { get; set; }
        public int? SectionId { get; set; }
        public string? DesignationName { get; set; }
        public int? DesignationId { get; set; }
        public int? AdditionalResponsibilityId { get; set; }
        public string? AdditionalResponsibilityName { get; set; }
        public string? EmpPhotoName { get; set; }
        public string? EmpGenderName { get; set; }
        public bool?  IsAdditionalDesignation { get; set; }
        public DateOnly? JoiningDate { get; set; }
    }
}
