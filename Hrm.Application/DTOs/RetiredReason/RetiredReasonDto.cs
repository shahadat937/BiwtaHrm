using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RetiredReason
{
    public class RetiredReasonDto : IRetiredReasonDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IdNeeded { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
