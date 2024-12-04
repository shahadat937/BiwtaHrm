using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.JobDetailsSetup;


namespace Hrm.Application.Features.JobDetailsSetups.Requests.Commands
{
    public class CreateJobDetailsSetupCommand : IRequest<BaseCommandResponse> 
    {
        public CreateJobDetailsSetupDto JobDetailsSetupDto { get; set; }

    }
}
