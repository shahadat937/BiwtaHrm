using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Occupation
{
    public class OccupationDto: IOccupationDto
    {
        public int OccupationId { get; set; }
        public string? OccupationName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
