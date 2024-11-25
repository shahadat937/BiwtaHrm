using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpSpouseInfo
{
    public class CreateEmpSpouseInfoDto : IEmpSpouseInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string? SpouseName { get; set; }
        public string? SpouseNameBangla { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? BirthRegNo { get; set; }
        public string? NID { get; set; }
        public int? OccupationId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
