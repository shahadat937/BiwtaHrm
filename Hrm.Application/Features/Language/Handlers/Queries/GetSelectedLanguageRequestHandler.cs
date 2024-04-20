using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Language.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Language.Handlers.Queries
{ 
    public class GetSelectedLanguageRequestHandler : IRequestHandler<GetSelectedLanguageRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Language> _LanguageRepository;


        public GetSelectedLanguageRequestHandler(IHrmRepository<Hrm.Domain.Language> LanguageRepository)
        {
            _LanguageRepository = LanguageRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedLanguageRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Language> Languages = await _LanguageRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Languages.Select(x => new SelectedModel 
            {
                Name = x.LanguageName,
                Id = x.LanguageId
            }).ToList();
            return selectModels;
        }
    }
}
 