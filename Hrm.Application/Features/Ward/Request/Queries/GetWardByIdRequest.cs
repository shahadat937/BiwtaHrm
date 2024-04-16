using Hrm.Application.DTOs.Ward;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Requests.Queries
{
    public class GetWardByIdRequest : IRequest<WardDto>
    {
        public int WardId { get; set; }
    }
}
