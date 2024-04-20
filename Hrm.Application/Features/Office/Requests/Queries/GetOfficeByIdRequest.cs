using Hrm.Application.DTOs.Office;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Requests.Queries
{
    public class GetOfficeByIdRequest : IRequest<OfficeDto>
    {
        public int OfficeId { get; set; }
    }
}
