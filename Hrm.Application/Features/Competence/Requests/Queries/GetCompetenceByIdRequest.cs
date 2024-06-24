using Hrm.Application.DTOs.Competence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Competence.Requests.Queries
{
    public class GetCompetenceByIdRequest : IRequest<CompetenceDto>
    {
        public int CompetenceId { get; set; }
    }
}
