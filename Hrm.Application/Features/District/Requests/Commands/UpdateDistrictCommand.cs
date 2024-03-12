using Hrm.Application.DTOs.District;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Requests.Commands
{
    public class UpdateDistrictCommand : IRequest<BaseCommandResponse>
    {
        public DistrictDto DistrictDto { get; set; }
    }
}
