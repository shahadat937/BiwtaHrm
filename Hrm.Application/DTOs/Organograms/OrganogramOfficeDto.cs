using Hrm.Application.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramOfficeDto
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public List<OrganogramDepartmentDto> Departments { get; set; }
    }
}
