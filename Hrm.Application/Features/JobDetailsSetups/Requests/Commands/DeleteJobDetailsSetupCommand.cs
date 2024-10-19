using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.JobDetailsSetups.Requests.Commands
{
    public class DeleteJobDetailsSetupCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
