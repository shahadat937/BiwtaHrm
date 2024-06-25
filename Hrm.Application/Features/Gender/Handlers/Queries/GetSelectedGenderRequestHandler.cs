using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Gender.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Handlers.Queries
{
    public class GetSelectedGenderRequestHandler : IRequestHandler<GetSelectedGenderRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Gender> _GenderRepository;


        public GetSelectedGenderRequestHandler(IHrmRepository<Hrm.Domain.Gender> GenderRepository)
        {
            _GenderRepository = GenderRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedGenderRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Gender> Genders = await _GenderRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Genders.Select(x => new SelectedModel
            {
                Name = x.GenderName,
                Id = x.GenderId
            }).ToList();
            return selectModels;
        }
    }
}
