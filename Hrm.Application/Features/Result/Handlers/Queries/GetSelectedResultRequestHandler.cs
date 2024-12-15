using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Results.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Results.Handlers.Queries
{ 
    public class GetSelectedResultRequestHandler : IRequestHandler<GetSelectedResultRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Result> _ResultRepository;


        public GetSelectedResultRequestHandler(IHrmRepository<Hrm.Domain.Result> ResultRepository)
        {
            _ResultRepository = ResultRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedResultRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Result> Results = await _ResultRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Results.Select(x => new SelectedModel 
            {
                Name = x.ResultName,
                Id = x.ResultId
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
 