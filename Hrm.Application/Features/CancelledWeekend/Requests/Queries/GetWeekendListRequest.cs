using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CancelledWeekend.Requests.Queries
{
    public class GetWeekendListRequest: IRequest<object>
    {
        public int YearId { get; set; }
    }
}
