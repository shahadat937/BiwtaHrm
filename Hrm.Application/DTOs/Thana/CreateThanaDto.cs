using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana
{
    public class CreateThanaDto : IThanaDto
    {
        public int ThanaId { get; set; }
        public string? ThanaName { get; set; }
        public int? UpazilaId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
