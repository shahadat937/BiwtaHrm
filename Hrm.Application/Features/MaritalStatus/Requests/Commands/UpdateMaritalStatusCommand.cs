using Hrm.Application.DTOs.MaritalStatus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Requests.Commands
{
    public class UpdateMaritalStatusCommand : IRequest<Unit>
    {
        public MaritalStatusDto MaritalStatusDto { get; set; }
    }
}
