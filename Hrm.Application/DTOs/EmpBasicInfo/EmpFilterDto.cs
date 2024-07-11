using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpBasicInfo
{
    public class EmpFilterDto
    {
        public int? OfficeId;
        public int? DepartmentId;
        public int? ShiftId;
    }
}
