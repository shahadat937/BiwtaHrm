using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana_Upazila
{
    public class Thana_UpazilaDto : IThana_UpazilaDto
    {
        public int Thana_UpazilaId { get; set; }
        public required string Thana_UpazilaName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
