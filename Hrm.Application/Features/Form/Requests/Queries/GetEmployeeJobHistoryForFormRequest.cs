using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Requests.Queries
{
    public class GetEmployeeJobHistoryForFormRequest: IRequest<object>
    {
        public int EmpId { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
    }
}
