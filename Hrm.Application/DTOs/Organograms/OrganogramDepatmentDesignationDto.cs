using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
   public class OrganogramDepatmentDesignationDto
    {
        public List<OrganogramDesignationNameDto> Designations { get; set; }
        public List<OrganogramSectionNameDto> Sections { get; set; }
        public List<OrganogramDepartmentNameDto> SubDepartments { get; set; }
    }
}
