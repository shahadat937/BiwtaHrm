using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeletePunishmentCommand : IRequest
    {
        public int PunishmentId { get; set; }
    }
}
