using Hrm.Application.DTOs.Features;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.Features.Requests.Commands
{
    public class UpdateFeatureCommand : IRequest<BaseCommandResponse>  
    {
        public CreateFeatureDto FeatureDto { get; set; }
    }
}
