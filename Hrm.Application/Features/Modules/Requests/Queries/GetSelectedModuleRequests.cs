using MediatR;
using Hrm.Shared.Models;
using System.Collections.Generic;

namespace Hrm.Application.Features.Modules.Requests.Queries
{
    public class GetSelectedModuleRequests : IRequest<List<SelectedModel>>
    {
        public string Type { get; set; }  
    } 
}
