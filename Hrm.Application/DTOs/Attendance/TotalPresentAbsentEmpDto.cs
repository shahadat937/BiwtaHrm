using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class TotalPresentAbsentEmpDto
    {
        public DateOnly Date {  get; set; }
        public int? totalPresentEmp {  get; set; }
        public int? totalAbsentEmp {  get; set; }
    }
}
