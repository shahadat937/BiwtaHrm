using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Designation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? OfficeId { get; set; }
        public int? UpperDepartmentId { get; set; }
        public List<OrganogramDesignationDto> Designations { get; set; }
        public List<OrganogramDepartmentDto> SubDepartments { get; set; }
    }
}
