using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroup.Requests.Commands
{
    public class DeleteBloodGroupCommand : IRequest<BaseCommandResponse>
    {
        public int BloodGroupId { get; set; }
    }
}
