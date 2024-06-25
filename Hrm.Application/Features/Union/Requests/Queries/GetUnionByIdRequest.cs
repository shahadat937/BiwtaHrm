using Hrm.Application.DTOs.Union;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Requests.Queries
{
    public class GetUnionByIdRequest : IRequest<UnionDto>
    {
        public int UnionId { get; set; }
    }
}
