using Hrm.Application.DTOs.Division;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Requests.Queries
{
    public class GetDivisionByIdRequest : IRequest<DivisionDto>
    {
        public int DivisionId { get; set; }
    }
}
