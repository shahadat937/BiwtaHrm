using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.RetiredReasons.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RetiredReasons.Handlers.Queries
{ 
    public class GetSelectedRetiredReasonRequestHandler : IRequestHandler<GetSelectedRetiredReasonRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.RetiredReason> _RetiredReasonRepository;


        public GetSelectedRetiredReasonRequestHandler(IHrmRepository<Hrm.Domain.RetiredReason> RetiredReasonRepository)
        {
            _RetiredReasonRepository = RetiredReasonRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedRetiredReasonRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.RetiredReason> RetiredReasons = await _RetiredReasonRepository.FilterAsync(x => x.IsActive == true);
            List<SelectedModel> selectModels = RetiredReasons.Select(x => new SelectedModel 
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 