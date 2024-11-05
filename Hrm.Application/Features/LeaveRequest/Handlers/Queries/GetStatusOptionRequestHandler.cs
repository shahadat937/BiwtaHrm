using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetStatusOptionRequestHandler : IRequestHandler<GetLeaveStatusOptionRequest, object>
    {
        public GetStatusOptionRequestHandler() { 
        
        }

        public async Task<object> Handle(GetLeaveStatusOptionRequest request, CancellationToken cancellationToken)
        {
            List<string> options = new List<string>
            {
                "Pending", "Not Recommended", "Recommended", "Approved", "Denied"
            };

            return options;
        }
    }
}
