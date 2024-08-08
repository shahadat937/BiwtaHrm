using Hrm.Application.DTOs.LeaveRequest;
using Hrm.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Requests.Commands
{
    public class CreateLeaveRequestCommand: IRequest<BaseCommandResponse>
    {
        public CreateLeaveRequestDto createLeaveRequestDto { get; set; }
        public IFormFile? AssociatedFiles { get; set; }
    }
}
