using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BloodGroup
{
    public interface IBloodGroupDto
    {
        public int BloodGroupId { get; set; }
        public string? BloodGroupName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
