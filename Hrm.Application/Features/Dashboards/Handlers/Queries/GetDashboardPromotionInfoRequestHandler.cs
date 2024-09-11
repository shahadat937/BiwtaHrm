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
    public class GetDashboardPromotionInfoRequestHandler : IRequestHandler<GetDashboardPromotionInfoRequest, object>
    {
        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IMapper _mapper;

        public GetDashboardPromotionInfoRequestHandler(
            IMapper mapper,
            IHrmRepository<EmpPromotionIncrement> EmpPromotionIncrementRepository)
        {
            _mapper = mapper;
            _EmpPromotionIncrementRepository = EmpPromotionIncrementRepository;

        }

        public async Task<object> Handle(GetDashboardPromotionInfoRequest request, CancellationToken cancellationToken)
        {

            var totalApplications = await _EmpPromotionIncrementRepository.CountAsync(e => true);

            var pendingApplications = await _EmpPromotionIncrementRepository.CountAsync(e => e.ApplicationStatus == null);

            var approvedApplications = await _EmpPromotionIncrementRepository.CountAsync(e => e.ApplicationStatus == true);

            var rejectedApplications = await _EmpPromotionIncrementRepository.CountAsync(e => e.ApplicationStatus == false);

            var widgets = new List<WidgetsDto>
            {
                new WidgetsDto
                {
                    WidgetName = "Total Promotion/Increment Applications",
                    Label = "Total Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { totalApplications }
                },
                new WidgetsDto
                {
                    WidgetName = "Pending Promotion/Increment Applications",
                    Label = "Pending Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { pendingApplications }
                },
                new WidgetsDto
                {
                    WidgetName = "Approved Promotion/Increment Applications",
                    Label = "Approved Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { approvedApplications }
                },
                new WidgetsDto
                {
                    WidgetName = "Rejected Promotion/Increment Applications",
                    Label = "Rejected Applications",
                    Labels = new List<string>(),
                    Data = new List<int> { rejectedApplications }
                }
            };

            return widgets;
        }
    }
}
