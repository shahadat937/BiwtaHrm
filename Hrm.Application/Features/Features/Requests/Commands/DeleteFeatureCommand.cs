using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.Features.Requests.Commands
{
    public class DeleteFeatureCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
