using Hrm.Application.DTOs.Workday;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Requests.Queries
{
    public class GetWorkdayByIdRequest : IRequest<WorkdayDto>
    {
        public int WorkdayId { get; set; }
    }
}
