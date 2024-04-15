using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Scale.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scale.Handlers.Queries
{

    public class GetSelectScaleRequestHandler : IRequestHandler<GetSelectScaleRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Scale> _ScaleRepository;


        public GetSelectScaleRequestHandler(IHrmRepository<Hrm.Domain.Scale> ScaleRepository)
        {
            _ScaleRepository = ScaleRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectScaleRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Scale> Scales = await _ScaleRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Scales.Select(x => new SelectedModel
            {
                Name = x.ScaleName,
                Id = x.ScaleId
            }).ToList();
            return selectModels;
        }
    }
}
