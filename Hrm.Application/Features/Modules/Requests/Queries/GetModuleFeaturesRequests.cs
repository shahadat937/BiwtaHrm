using hrm.Application.DTOs.Modules;
using MediatR;


namespace Hrm.Application.Features.Modules.Requests.Queries
{
    public class GetModuleFeaturesRequests : IRequest<List<ModuleFeatureDto>>
    {
        public int FeatureType { get; set; }
    }
}
