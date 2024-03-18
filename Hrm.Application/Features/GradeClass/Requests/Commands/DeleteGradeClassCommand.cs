using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeClass.Requests.Commands
{
    public class DeleteGradeClassCommand : IRequest<BaseCommandResponse>
    {
        public int GradeClassId { get; set; }
    }
}
