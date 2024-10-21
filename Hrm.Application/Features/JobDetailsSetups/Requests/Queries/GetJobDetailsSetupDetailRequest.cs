using Hrm.Application.DTOs.JobDetailsSetup;
using MediatR;


namespace Hrm.Application.Features.JobDetailsSetups.Requests.Queries
{
    public class GetJobDetailsSetupDetailRequest : IRequest<JobDetailsSetupDto>
    {
        public int Id { get; set; }
    }
}
