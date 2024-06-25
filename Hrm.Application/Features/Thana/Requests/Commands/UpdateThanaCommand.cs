using Hrm.Application.DTOs.Thana;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.Thana.Requests.Commands
{
    public class UpdateThanaCommand : IRequest<BaseCommandResponse>
    {
        public required ThanaDto ThanaDto { get; set; }
    }
}
