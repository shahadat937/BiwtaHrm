using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Division
{
    public  class CreateDivisionDto:IDivisionDto
    {
        public int DivisionId { get; set; }
        public string? DivisionName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
