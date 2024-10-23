using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.NavbarThem;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarThems.Requests.Commands
{
    public class CreateNavbarThemCommand : IRequest<BaseCommandResponse>
    {
        public CreateNavbarThemDto NavbarThemDto { get; set; }
    }
}
