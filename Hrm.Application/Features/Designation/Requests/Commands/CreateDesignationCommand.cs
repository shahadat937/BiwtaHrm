using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Requests.Commands
{
    public class CreateDesignationCommand : IRequest<BaseCommandResponse>
    {
        public CreateDesignationDto DesignationDto { get; set; }
    }
}
