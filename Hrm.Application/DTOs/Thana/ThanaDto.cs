using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana
{
    public class ThanaDto : IThanaDto
    {
        public int ThanaId { get; set; }
        public required string ThanaName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
