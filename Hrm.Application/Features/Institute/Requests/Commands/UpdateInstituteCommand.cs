using Hrm.Application.DTOs.Institute;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.Institute.Requests.Commands
{
    public class UpdateInstituteCommand : IRequest<BaseCommandResponse>
    {
        public required InstituteDto InstituteDto { get; set; }
    }
}
