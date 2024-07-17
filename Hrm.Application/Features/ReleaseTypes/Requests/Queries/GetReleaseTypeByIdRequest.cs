using Hrm.Application.DTOs.ReleaseType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ReleaseTypes.Requests.Queries
{
    public class GetReleaseTypeByIdRequest : IRequest<ReleaseTypeDto>
    {
        public int ReleaseTypeId { get; set; }
    }
}
