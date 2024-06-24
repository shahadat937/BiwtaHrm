using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Handlers.Queries
{
    public class GetSelectedMaritalStatusRequestHandler : IRequestHandler<GetSelectedMaritalStatusRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.MaritalStatus> _MaritalStatusRepository;


        public GetSelectedMaritalStatusRequestHandler(IHrmRepository<Hrm.Domain.MaritalStatus> MaritalStatusRepository)
        {
            _MaritalStatusRepository = MaritalStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedMaritalStatusRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.MaritalStatus> MaritalStatuss = await _MaritalStatusRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = MaritalStatuss.Select(x => new SelectedModel
            {
                Name = x.MaritalStatusName,
                Id = x.MaritalStatusId
            }).ToList();
            return selectModels;
        }
    }
}
