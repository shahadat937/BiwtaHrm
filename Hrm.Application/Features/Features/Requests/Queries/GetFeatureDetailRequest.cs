using Hrm.Application.DTOs.Features;
using MediatR;


namespace Hrm.Application.Features.Features.Requests.Queries
{
    public class GetFeatureDetailRequest : IRequest<FeatureDto>
    {
        public int Id { get; set; }
    }
}
