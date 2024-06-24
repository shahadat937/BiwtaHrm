using Hrm.Application.DTOs.Office;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Requests.Commands
{
    public class UpdateOfficeCommand : IRequest<BaseCommandResponse>
    {
        public OfficeDto OfficeDto { get; set; }
    }
}
