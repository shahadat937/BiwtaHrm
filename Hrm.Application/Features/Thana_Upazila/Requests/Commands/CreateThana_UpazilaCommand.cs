using Hrm.Application.DTOs.Thana_Upazila;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana_Upazila.Requests.Commands
{
    public class CreateThana_UpazilaCommand : IRequest<BaseCommandResponse>
    {
        public CreateThana_UpazilaDto Thana_UpazilaDto { get; set; }
    }
}
