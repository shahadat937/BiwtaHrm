using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Department
{
    public class CreateDepartmentDto:IDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
