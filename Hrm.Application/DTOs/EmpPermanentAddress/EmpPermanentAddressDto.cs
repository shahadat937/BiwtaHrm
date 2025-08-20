using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpPermanentAddress
{
    public class EmpPermanentAddressDto : IEmpPermanentAddressDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? CountryId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? UpazilaId { get; set; }
        public int? ThanaId { get; set; }
        public string? UnionName { get; set; }
        public string? WardName { get; set; }
        public string? PostOffice { get; set; }
        public int? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public string? CountryName { get; set; }
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? UpazilaName { get; set; }
        public string? ThanaName { get; set; }
    }
}
