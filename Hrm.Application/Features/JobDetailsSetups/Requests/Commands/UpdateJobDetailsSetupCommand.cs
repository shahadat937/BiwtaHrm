using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.JobDetailsSetups.Requests.Commands
{
    public class UpdateJobDetailsSetupCommand : IRequest<BaseCommandResponse>  
    {
        public CreateJobDetailsSetupDto JobDetailsSetupDto { get; set; }
    }
}
