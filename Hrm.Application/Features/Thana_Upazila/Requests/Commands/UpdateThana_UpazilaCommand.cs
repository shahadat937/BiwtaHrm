using Hrm.Application.DTOs.Thana_Upazila;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.Thana_Upazila.Requests.Commands
{
    public class UpdateThana_UpazilaCommand : IRequest<BaseCommandResponse>
    {
        public required Thana_UpazilaDto Thana_UpazilaDto { get; set; }
    }
}
