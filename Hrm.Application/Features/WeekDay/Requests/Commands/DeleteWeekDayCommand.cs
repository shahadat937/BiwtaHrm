using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteWeekDayCommand : IRequest
    {
        public int WeekendId { get; set; }
    }
}
