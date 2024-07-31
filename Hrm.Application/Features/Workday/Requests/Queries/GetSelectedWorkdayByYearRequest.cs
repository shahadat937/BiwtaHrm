using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Requests.Queries
{
    public class GetSelectedWorkdayByYearRequest: IRequest<object>
    {
        public int yearId { get; set; }
    }
}
