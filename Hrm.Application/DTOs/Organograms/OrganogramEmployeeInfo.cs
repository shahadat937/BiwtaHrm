using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramEmployeeInfo
    {
        public string Name { get; set; }
        public  EmployeeInfo? EmployeeInfo { get; set; }
    }

    public class EmployeeInfo
    {
        public int EmpId { get; set; }
        public string? EmployeeName { get; set; }
    }
}
