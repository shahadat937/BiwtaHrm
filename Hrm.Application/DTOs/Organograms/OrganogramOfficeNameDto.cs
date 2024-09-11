using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramOfficeNameDto
    {
        public string Name { get; set; }
        public List<OrganogramDesignationNameDto> DirectDesignations { get; set; }
        public List<OrganogramDepartmentNameDto> Departments { get; set; }
        public List<OrganogramSectionNameDto> Sections { get; set; }
    }
}
