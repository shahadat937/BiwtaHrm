using Hrm.Application.DTOs.SubDepartment;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Requests.Commands
{
    public class CreateSubDepartmentCommand :IRequest<BaseCommandResponse>
    {
        public CreateSubDepartmentDto SubDepartmentDto { get; set; }
    }
}
