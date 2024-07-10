using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpBasicInfo
{
    public interface IEmpBasicInfoDto
    {
        public int Id { get; set; }
        public int IdCardNo { get; set; }
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
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
