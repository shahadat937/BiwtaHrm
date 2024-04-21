using Hrm.Application.DTOs.Institute;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Requests.Queries
{
    public class GetInstituteByIdRequest : IRequest<InstituteDto>
    {
        public int InstituteId { get; set; }
    }
}
