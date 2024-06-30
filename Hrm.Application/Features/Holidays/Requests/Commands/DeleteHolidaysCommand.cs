using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Requests.Commands
{
    public class DeleteHolidaysCommand:IRequest<BaseCommandResponse>
    {
        public int HolidayId { get; set; }
    }
}
