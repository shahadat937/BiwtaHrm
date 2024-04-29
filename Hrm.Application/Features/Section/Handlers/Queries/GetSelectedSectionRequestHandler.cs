using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Section.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Handlers.Queries
{ 
    public class GetSelectedSectionRequestHandler : IRequestHandler<GetSelectedSectionRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;


        public GetSelectedSectionRequestHandler(IHrmRepository<Hrm.Domain.Section> SectionRepository)
        {
            _SectionRepository = SectionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSectionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Section> Sections = await _SectionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Sections.Select(x => new SelectedModel 
            {
                Name = x.SectionName,
                Id = x.SectionId
            }).ToList();
            return selectModels;
        }
    }
}
 