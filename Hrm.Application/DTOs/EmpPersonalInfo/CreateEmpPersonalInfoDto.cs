using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpPersonalInfo
{
    public class CreateEmpPersonalInfoDto : IEmpPersonalInfoDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? BloodGroupId { get; set; }
        public int? NationalityId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public int? BirthRegNo { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? FreedomfighterStatus { get; set; }
        public int? ReligionId { get; set; }
        public int? TinNo { get; set; }
        public int? HairColorId { get; set; }
        public int? EyesColorId { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public int? HealthIssueStatusId { get; set; }
        public int? DrivingLicenceNo { get; set; }
        public DateOnly? DrivingLicenceDate { get; set; }
        public string? PassportNo { get; set; }
        public DateOnly? PassportExpireDate { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
