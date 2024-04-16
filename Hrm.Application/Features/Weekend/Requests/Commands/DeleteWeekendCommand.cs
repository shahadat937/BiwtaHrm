using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteWeekendCommand : IRequest
    {
        public int WeekendId { get; set; }
    }
}
