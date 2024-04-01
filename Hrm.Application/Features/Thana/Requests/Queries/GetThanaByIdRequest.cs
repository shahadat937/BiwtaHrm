using Hrm.Application.DTOs.Thana;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Requests.Queries
{
    public class GetThanaByIdRequest : IRequest<ThanaDto>
    {
        public int ThanaId { get; set; }
    }
}
