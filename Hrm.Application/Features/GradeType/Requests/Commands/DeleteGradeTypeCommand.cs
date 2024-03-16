using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeType.Requests.Commands
{
    public class DeleteGradeTypeCommand : IRequest<BaseCommandResponse>
    {
        public int GradeTypeId { get; set; }
    }
}
