using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FinancialYears.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FinancialYears.Handlers.Queries
{
    public class GetSelectedFinancialYearRequestHandler : IRequestHandler<GetSelectedFinancialYearRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.FinancialYear> _FinancialYearRepository;


        public GetSelectedFinancialYearRequestHandler(IHrmRepository<Hrm.Domain.FinancialYear> FinancialYearRepository)
        {
            _FinancialYearRepository = FinancialYearRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFinancialYearRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.FinancialYear> FinancialYears = await _FinancialYearRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = FinancialYears.Select(x => new SelectedModel
            {
                Name = x.YearName,
                Id = x.Id
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
