using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Dashboard.Chart;
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
    public class GetDashboardFieldUnfieldInfoRequestHandler : IRequestHandler<GetDashboardFieldUnfieldInfoRequest, object>
    {
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IHrmRepository<Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;

        public GetDashboardFieldUnfieldInfoRequestHandler(
            IMapper mapper,
            IHrmRepository<EmpJobDetail> EmpJobDetailRepository,
            IHrmRepository<Domain.Designation> DesignationRepository)
        {
            _mapper = mapper;
            _EmpJobDetailRepository = EmpJobDetailRepository;
            _DesignationRepository = DesignationRepository;

        }

        public async Task<object> Handle(GetDashboardFieldUnfieldInfoRequest request, CancellationToken cancellationToken)
        {

            var designations = await _DesignationRepository.GetAll();

            var empJobDetails = await _EmpJobDetailRepository.GetAll();

            var fieldCount = empJobDetails.Select(e => e.DesignationId).Distinct().Count();

            var totalDesignationCount = designations.Count();

            var unfieldCount = totalDesignationCount - fieldCount;

            var chartWidgetsDto = new ChartWidgetsDto
            {
                Labels = new List<string> { "Field", "Unfield" },
                Datasets = new List<ChartWidgetsDataSets>
                {
                    new ChartWidgetsDataSets
                    {
                        Data = new List<int> { fieldCount, unfieldCount },
                        BackgroundColor = new List<string> { "#41B883", "#DD1B16" } // Example colors
                    }
                }
            };

            return chartWidgetsDto;
        }
    }
}