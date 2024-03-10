using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana_Upazila.Requests.Commands
{
    public class DeleteThana_UpazilaCommand : IRequest<BaseCommandResponse>
    {
        public int Thana_UpazilaId { get; set; }
    }
}
