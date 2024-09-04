using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpShiftAssign
{
    public class CreateEmpShiftAssignDto : IEmpShiftAssignDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? ShiftId { get; set; }
        public bool? IsActive { get; set; }
    }
}
