using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana_Upazila
{
    public interface IThana_UpazilaDto
    {
        public int Thana_UpazilaId { get; set; }
        public string? Thana_UpazilaName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
