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
    public class UpdateSubDepartmentCommand : IRequest<BaseCommandResponse>
    {
        public SubDepartmentDto SubDepartmentDto { get; set; }
    }
}
