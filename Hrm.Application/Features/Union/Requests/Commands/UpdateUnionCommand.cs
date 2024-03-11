using Hrm.Application.DTOs.Union;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.Union.Requests.Commands
{
    public class UpdateUnionCommand : IRequest<BaseCommandResponse>
    {
        public required UnionDto UnionDto { get; set; }
    }
}
