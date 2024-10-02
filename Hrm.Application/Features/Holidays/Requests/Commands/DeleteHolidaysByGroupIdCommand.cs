using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Requests.Commands
{
    public class DeleteHolidaysByGroupIdCommand: IRequest<BaseCommandResponse>
    {
        public int GroupId { get; set; }
    }
}
