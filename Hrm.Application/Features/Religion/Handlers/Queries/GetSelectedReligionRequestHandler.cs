using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Religion.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Religion.Handlers.Queries
{
    public class GetSelectedReligionRequestHandler : IRequestHandler<GetSelectedReligionRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Religion> _ReligionRepository;


        public GetSelectedReligionRequestHandler(IHrmRepository<Hrm.Domain.Religion> ReligionRepository)
        {
            _ReligionRepository = ReligionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReligionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Religion> Religions = await _ReligionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Religions.Select(x => new SelectedModel
            {
                Name = x.ReligionName,
                Id = x.ReligionId
            }).ToList();
            return selectModels;
        }
    }
}
