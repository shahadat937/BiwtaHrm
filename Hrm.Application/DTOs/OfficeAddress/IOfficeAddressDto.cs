using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OfficeAddress
{
    public interface IOfficeAddressDto
    {
        public int OfficeAddressId { get; set; }
        public string OfficeAddressName { get; set; }
        public int? CountryId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? UpazilaId { get; set; }
        public int? ThanaId { get; set; }
        public int? UnionId { get; set; }
        public int? WardId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
