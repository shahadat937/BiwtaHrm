using Hrm.Application.DTOs.MaritalStatus;
using MediatR;
namespace Hrm.Application.Features.MaritalStatus.Requests.Commands
{
    public class UpdateMaritalStatusCommand : IRequest<Unit>
    {
        public required MaritalStatusDto MaritalStatusDto { get; set; }
    }
}
