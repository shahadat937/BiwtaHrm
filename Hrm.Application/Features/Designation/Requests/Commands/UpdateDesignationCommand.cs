using Hrm.Application.DTOs.Designation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Requests.Commands
{
    public class UpdateDesignationCommand : IRequest<Unit>
    {
        public DesignationDto DesignationDto { get; set; }
    }
}
