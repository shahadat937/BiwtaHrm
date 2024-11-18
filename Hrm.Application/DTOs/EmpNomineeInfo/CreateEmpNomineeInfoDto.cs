using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpNomineeInfo
{
    public class CreateEmpNomineeInfoDto : IEmpNomineeInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string? PNo { get; set; }
        public string? NomineeName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? BirthRegNo { get; set; }
        public string? NID { get; set; }
        public int? RelationId { get; set; }
        public int? Percentage { get; set; }
        public string? Address { get; set; }
        public string? UniqueIdentity { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string? SignatureUrl { get; set; }
        public IFormFile? SignatureFile { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
