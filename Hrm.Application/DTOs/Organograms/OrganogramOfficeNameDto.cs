using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramOfficeNameDto
    {
        public string OfficeName { get; set; }
        public List<OrganogramDepartmentNameDto> Departments { get; set; }
    }
}
