using Hrm.Application.DTOs.Department;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Requests.Queries
{
    public class GetDepartmentsByOfficeIdRequest : IRequest<object>
    {
        public int OfficeId { get; set; }
    }
}
