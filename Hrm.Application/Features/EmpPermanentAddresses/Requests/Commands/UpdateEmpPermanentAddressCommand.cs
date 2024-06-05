using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.EmpPermanentAddress;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPermanentAddresses.Requests.Commands
{
    public class UpdateEmpPermanentAddressCommand : IRequest<BaseCommandResponse>
    {
        public EmpPermanentAddressDto EmpPermanentAddressDto { get; set; }
    }
}

