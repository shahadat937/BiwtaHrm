using Hrm.Application.DTOs.Subject;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Subject.Requests.Commands
{
    public class UpdateSubjectCommand : IRequest<BaseCommandResponse>
    {
        public SubjectDto SubjectDto { get; set; }
    }
}
