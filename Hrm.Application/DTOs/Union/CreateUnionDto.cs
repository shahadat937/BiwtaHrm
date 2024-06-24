using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Union
{
    public class CreateUnionDto : IUnionDto
    {
        public int UnionId { get; set; }
        public string? UnionName { get; set; }
        public int? ThanaId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
