using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.Upazila.Requests.Commands
{
    public class UpdateUpazilaCommand : IRequest<BaseCommandResponse>
    {
        public required UpazilaDto UpazilaDto { get; set; }
    }
}
