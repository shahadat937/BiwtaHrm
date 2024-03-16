using Hrm.Application.DTOs.BloodGroup;
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
    public class CreateSubjectCommand : IRequest<BaseCommandResponse>
    {
        public CreateSubjectDto SubjectDto { get; set; }
    }
}
