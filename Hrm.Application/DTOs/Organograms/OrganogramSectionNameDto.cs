using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramSectionNameDto
    {
        public string Name { get; set; }
        public List<OrganogramDesignationNameDto> Designations { get; set; }
        public List<OrganogramSectionNameDto> SubSections { get; set; }
    }
}
