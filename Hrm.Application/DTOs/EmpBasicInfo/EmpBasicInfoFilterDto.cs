using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpBasicInfo
{
    public class EmpBasicInfoFilterDto
    {
        public int? DepartmentId {  get; set; }
        public int? SectionId {  get; set; }
        public int? ShiftId { get; set; }
        public int? DesignationId { get; set; }
        public int? OfficeId {  get; set; }
        public int? OfficeBranchId {  get; set; }
    }
}
