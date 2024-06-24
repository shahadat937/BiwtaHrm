using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade.Requests.Commands
{
    public class DeleteGradeCommand : IRequest<BaseCommandResponse>
    {
        public int GradeId { get; set; }
    }
}
