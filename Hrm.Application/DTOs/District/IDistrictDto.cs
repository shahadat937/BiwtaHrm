using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.District
{
    public interface IDistrictDto
    {
        public int DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
