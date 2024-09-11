using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Dashboard.Chart;
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
    public class GetDashboardGenderInfoRequestHandler : IRequestHandler<GetDashboardGenderInfoRequest, object>
    {
        private readonly IHrmRepository<EmpPersonalInfo> _EmpPersonalInfoRepository;
        private readonly IHrmRepository<Domain.Gender> _GenderRepository;
        private readonly IMapper _mapper;

        public GetDashboardGenderInfoRequestHandler(
            IMapper mapper,
            IHrmRepository<EmpPersonalInfo> EmpPersonalInfoRepository,
            IHrmRepository<Domain.Gender> GenderRepository)
        {
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _GenderRepository = GenderRepository;

        }

        public async Task<object> Handle(GetDashboardGenderInfoRequest request, CancellationToken cancellationToken)
        {
            var genders = await _GenderRepository.GetAll();
            var labels = new List<string>();
            var data = new List<int>();

            var backgroundColors = new List<string> { "#4BC0C0", "#FF6384", "#FFCE56", "#36A2EB", "#9966FF", "#FF9F40" };

            foreach (var gender in genders)
            {
                labels.Add(gender.GenderName);

                var employeeCountByGender = await _EmpPersonalInfoRepository.CountAsync(e => e.GenderId == gender.GenderId);
                data.Add(employeeCountByGender);
            }

            var chartWidgetsDto = new ChartWidgetsDto
            {
                Labels = labels,
                Datasets = new List<ChartWidgetsDataSets>
                {
                    new ChartWidgetsDataSets
                    {
                        Data = data,
                        BackgroundColor = backgroundColors.Take(data.Count).ToList()
                    }
                }
            };

            return chartWidgetsDto;
        }
    }
}