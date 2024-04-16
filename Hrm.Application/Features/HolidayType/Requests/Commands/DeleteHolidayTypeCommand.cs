using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteHolidayTypeCommand : IRequest
    {
        public int HolidayTypeId { get; set; }
    }
}
