using Hrm.Application.DTOs.AspNetUsers;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AspNetUsers.Requests.Commands
{
    public class CreateAspNetUserCommand : IRequest<BaseCommandResponse>
    {
        public CreateAspNetUserDto AspNetUserDto { get; set; }
    }
}
