using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteCompetenceCommand : IRequest
    {
        public int CompetenceId { get; set; }
    }
}
