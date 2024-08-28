using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpShiftAssign
{
    public class EmpShiftAssignDto : IEmpShiftAssignDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? ShiftId { get; set; }
        public bool? IsActive { get; set; }

        public string? PMISNo { get; set; }
        public string? EmpName { get; set; }
        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }
        public string? ShiftName { get; set; }
    }
}
