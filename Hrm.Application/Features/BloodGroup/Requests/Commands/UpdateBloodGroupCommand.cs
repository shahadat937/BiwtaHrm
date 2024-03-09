using Hrm.Application.DTOs.BloodGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroup.Requests.Commands
{
    public class UpdateBloodGroupCommand : IRequest<Unit>
    {
        public BloodGroupDto BloodGroupDto { get; set; }
    }
}
