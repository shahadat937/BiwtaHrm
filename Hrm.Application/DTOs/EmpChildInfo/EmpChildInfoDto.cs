using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpChildInfo
{
    public class EmpChildInfoDto : IEmpChildInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string? ChildName { get; set; }
        public string? ChildNameBangla { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int? BirthRegNo { get; set; }
        public string? NID { get; set; }
        public int? OccupationId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ChildStatusId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public string? OccupationName { get; set; }
        public string? GenderName { get; set; }
        public string? MaritalStatusName { get; set; }
        public string? ChildStatusName { get; set; }
    }
}
