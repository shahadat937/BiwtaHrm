using Hrm.Application.DTOs.Subject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Subject.Requests.Queries
{
    public class GetSubjectByIdRequest : IRequest<SubjectDto>
    {
        public int SubjectId { get; set; }
    }
}
