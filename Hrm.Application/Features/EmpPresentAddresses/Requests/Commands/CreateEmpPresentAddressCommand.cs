using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.EmpPresentAddress;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPresentAddresses.Requests.Commands
{
    public class CreateEmpPresentAddressCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpPresentAddressDto EmpPresentAddressDto { get; set; }
    }
}

