using Hrm.Application.DTOs.SubDepartment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Requests.Queries
{
    public class GetSubDepartmentByIdRequest : IRequest<SubDepartmentDto>
    {
        public int SubDepartmentId { get; set; }
    }
}
