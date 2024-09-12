using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Dashboard.Widgets;
using Hrm.Application.Features.Dashboards.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Dashboards.Handlers.Queries
{
    public class GetDashboardTransferInfoRequestHandler : IRequestHandler<GetDashboardTransferInfoRequest, object>
    {
        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;

        public GetDashboardTransferInfoRequestHandler(
            IMapper mapper,
            IHrmRepository<EmpTransferPosting> EmpTransferPostingRepository)
        {
            _mapper = mapper;
            _EmpTransferPostingRepository = EmpTransferPostingRepository;

        }

        public async Task<object> Handle(GetDashboardTransferInfoRequest request, CancellationToken cancellationToken)
        {

            var totalApplications = await _EmpTransferPostingRepository.CountAsync(e => true);

            var pendingApplications = await _EmpTransferPostingRepository.CountAsync(e => e.ApplicationStatus == null);

            var approvedApplications = await _EmpTransferPostingRepository.CountAsync(e => e.ApplicationStatus == true);

            var rejectedApplications = await _EmpTransferPostingRepository.CountAsync(e => e.ApplicationStatus == false);

            var widgets = new List<WidgetsDto>
            {
                new WidgetsDto
                {
                    WidgetName = "Total Transfer/Posting Applications",
                    Label = "Total Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { totalApplications }
                },
                new WidgetsDto
                {
                    WidgetName = "Pending Transfer/Posting Applications",
                    Label = "Pending Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { pendingApplications }
                },
                new WidgetsDto
                {
                    WidgetName = "Approveed Transfer/Posting Applications",
                    Label = "Approved Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { approvedApplications }
                },
                new WidgetsDto
                {
                    WidgetName = "Rejected Transfer/Posting Applications",
                    Label = "Rejected Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { rejectedApplications }
                }
            };

            return widgets;
        }
    }
}
