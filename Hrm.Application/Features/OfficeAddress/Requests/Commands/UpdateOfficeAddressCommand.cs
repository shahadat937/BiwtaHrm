using Hrm.Application.DTOs.OfficeAddress;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeAddress.Requests.Commands
{
    public class UpdateOfficeAddressCommand : IRequest<BaseCommandResponse>
    {
        public OfficeAddressDto OfficeAddressDto { get; set; }
    }
}
