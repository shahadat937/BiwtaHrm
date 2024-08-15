using MediatR;
using Hrm.Shared.Models;
using System.Collections.Generic;

namespace Hrm.Application.Features.Features.Requests.Queries
{
    public class GetSelectedFeatureRequests : IRequest<List<SelectedModel>>
    {
        public string Type { get; set; }  
    } 
}
