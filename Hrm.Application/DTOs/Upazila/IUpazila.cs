using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Upazila
{
    public interface IUpazilaDto
    {
        public int UpazilaId { get; set; }
        public string? UpazilaName { get; set; }
        public int? DistrictId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
