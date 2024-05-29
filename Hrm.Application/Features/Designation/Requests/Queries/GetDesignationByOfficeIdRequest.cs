using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Requests.Queries
{
    public class GetDesignationByOfficeIdRequest : IRequest<object>
    {
        public int OfficeId { get; set; }
    }
}
