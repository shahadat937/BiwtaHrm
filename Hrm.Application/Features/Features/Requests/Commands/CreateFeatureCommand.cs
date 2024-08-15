using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.Features;


namespace Hrm.Application.Features.Features.Requests.Commands
{
    public class CreateFeatureCommand : IRequest<BaseCommandResponse> 
    {
        public CreateFeatureDto FeatureDto { get; set; }

    }
}
