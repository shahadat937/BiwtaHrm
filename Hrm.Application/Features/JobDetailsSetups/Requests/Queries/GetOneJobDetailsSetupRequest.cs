using Hrm.Application.DTOs.JobDetailsSetup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetups.Requests.Queries
{
    public class GetOneJobDetailsSetupRequest : IRequest<JobDetailsSetupDto>
    {
    }
}
