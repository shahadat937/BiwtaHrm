using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Group
{
    public  class CreateGroupDto:IGroupDto
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
