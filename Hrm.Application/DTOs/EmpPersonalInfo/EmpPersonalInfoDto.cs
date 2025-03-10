﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpPersonalInfo
{
    public class EmpPersonalInfoDto : IEmpPersonalInfoDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? BloodGroupId { get; set; }
        public int? NationalityId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? BirthRegNo { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? FatherName { get; set; }
        public string? FatherNameBangla { get; set; }
        public string? MotherName { get; set; }
        public string? MotherNameBangla { get; set; }
        public string? GurdianName { get; set; }
        public string? GurdianNameBangla { get; set; }
        public int? GurdianRelationId { get; set; }
        public string? EmergencyContact { get; set; }
        public string? FreedomfighterStatus { get; set; }
        public int? ReligionId { get; set; }
        public string? TinNo { get; set; }
        public int? HairColorId { get; set; }
        public int? EyesColorId { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public int? HealthIssueStatusId { get; set; }
        public string? DrivingLicenceNo { get; set; }
        public DateOnly? DrivingLicenceDate { get; set; }
        public string? PassportNo { get; set; }
        public DateOnly? PassportExpireDate { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public string? GenderName { get; set; }
        public string? MaritalStatusName { get; set; }
        public string? BloodGroupName { get; set; }
        public string? ReligionName { get; set; }
        public string? HairColorName { get; set; }
        public string? EyesColorName { get; set; }
        public string? NationalityName { get; set; }
    }
}
