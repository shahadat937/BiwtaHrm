using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramDesignationNameDto
    {
        public string Name { get; set; }
        public OrganogramEmployeeInfoDto EmployeeInfo { get; set; }
    }
    public class OrganogramEmployeeInfoDto
    {
        public int EmpId { get; set; }
        public string? EmployeeName { get; set; }

    }
}
