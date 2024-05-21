using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Organograms
{
    public class OrganogramDesignationDto
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
