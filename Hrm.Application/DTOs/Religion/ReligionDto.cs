using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Religion
{
    public class ReligionDto: IReligionDto
    {
        public int ReligionId { get; set; }
        public string? ReligionName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
