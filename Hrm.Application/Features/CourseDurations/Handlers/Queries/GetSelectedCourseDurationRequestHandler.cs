using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.CourseDurations.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CourseDurations.Handlers.Queries
{ 
    public class GetSelectedCourseDurationRequestHandler : IRequestHandler<GetSelectedCourseDurationRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.CourseDuration> _CourseDurationRepository;


        public GetSelectedCourseDurationRequestHandler(IHrmRepository<Hrm.Domain.CourseDuration> CourseDurationRepository)
        {
            _CourseDurationRepository = CourseDurationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCourseDurationRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.CourseDuration> CourseDurations = await _CourseDurationRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = CourseDurations.Select(x => new SelectedModel 
            {
                Name = x.Name.ToString(),
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 