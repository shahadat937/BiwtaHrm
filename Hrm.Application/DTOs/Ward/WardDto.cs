using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Ward
{
    public class WardDto : IWardDto
    {
        public int WardId { get; set; }
        public string WardName { get; set; }
        public int? UnionId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
