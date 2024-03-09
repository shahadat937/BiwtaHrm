using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ChildStatus
{
    public class ChildStatusDto: IChildStatusDto
    {
        public int ChildStatusId { get; set; }
        public string? ChildStatusName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
