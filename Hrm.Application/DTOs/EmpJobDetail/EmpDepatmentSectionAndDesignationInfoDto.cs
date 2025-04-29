using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpJobDetail
{
    public class EmpDepatmentSectionAndDesignationInfoDto
    {
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId   { get; set; }
        public int? DesignationId { get; set; }
        public string? CombainedIds { get; set; }
        public int? ResponsibilityTypeId { get; set; }
    }
}
