using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Requests.Commands
{
    public class DeleteWorkdayCommand : IRequest<BaseCommandResponse>
    {
        public int WorkdayId { get; set; }
    }
}
