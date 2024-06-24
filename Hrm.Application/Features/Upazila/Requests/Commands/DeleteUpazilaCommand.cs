using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Requests.Commands
{
    public class DeleteUpazilaCommand : IRequest<BaseCommandResponse>
    {
        public int UpazilaId { get; set; }
    }
}
