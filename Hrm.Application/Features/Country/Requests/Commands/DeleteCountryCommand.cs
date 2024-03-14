using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteCountryCommand : IRequest
    {
        public int CountryId { get; set; }
    }
}
