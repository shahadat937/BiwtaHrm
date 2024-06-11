using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Handlers.Queries
{
    public class GetSelectedChildStatusRequestHandler : IRequestHandler<GetSelectedChildStatusRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.ChildStatus> _ChildStatusRepository;


        public GetSelectedChildStatusRequestHandler(IHrmRepository<Hrm.Domain.ChildStatus> ChildStatusRepository)
        {
            _ChildStatusRepository = ChildStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedChildStatusRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.ChildStatus> ChildStatuss = await _ChildStatusRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = ChildStatuss.Select(x => new SelectedModel
            {
                Name = x.ChildStatusName,
                Id = x.ChildStatusId
            }).ToList();
            return selectModels;
        }
    }
}