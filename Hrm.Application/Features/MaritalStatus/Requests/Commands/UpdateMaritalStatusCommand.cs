using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.MaritalStatus.Requests.Commands
{
    public class UpdateMaritalStatusCommand : IRequest<BaseCommandResponse>
    {
        public required MaritalStatusDto MaritalStatusDto { get; set; }
    }
}
