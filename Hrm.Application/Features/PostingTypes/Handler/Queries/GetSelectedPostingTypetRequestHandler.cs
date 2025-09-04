using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.PostingTypes.Request.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingTypes.Handlers.Queries
{ 
    public class GetSelectedPostingTypeRequestHandler : IRequestHandler<GetSelectedPostingTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.PostingType> _PostingTypeRepository;


        public GetSelectedPostingTypeRequestHandler(IHrmRepository<Hrm.Domain.PostingType> PostingTypeRepository)
        {
            _PostingTypeRepository = PostingTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPostingTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.PostingType> PostingTypes = await _PostingTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = PostingTypes.Select(x => new SelectedModel 
            {
                Name = x.TypeName,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 