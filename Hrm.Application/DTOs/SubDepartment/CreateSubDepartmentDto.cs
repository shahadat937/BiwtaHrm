using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubDepartment
{
    public class CreateSubDepartmentDto: ISubDepartmentDto
    {
        public int SubDepartmentId { get; set; }
        public string? SubDepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}

