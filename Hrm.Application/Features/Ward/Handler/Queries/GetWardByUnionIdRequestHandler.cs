using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.Features.Union.Requests.Queries;
using Hrm.Application.Features.Ward.Request.Queries;
using Hrm.Application.Features.Ward.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Handler.Queries
{
    public class GetWardByUnionIdRequestHandler : IRequestHandler<GetWardByUnionIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Ward> _WardRepository;
        private readonly IMapper _mapper;
        public GetWardByUnionIdRequestHandler(IHrmRepository<Hrm.Domain.Ward> WardRepositoy, IMapper mapper)
        {
            _WardRepository = WardRepositoy;
            _mapper = mapper;
        }

        public async Task<List<SelectedModel>> Handle(GetWardByUnionIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Ward> Wards = _WardRepository.FilterWithInclude(x => x.UnionId == request.UnionId).ToList();
            List<SelectedModel> SelectedModels = Wards.Select(x => new SelectedModel
            {
                Id = x.WardId,
                Name = x.WardName
            }).ToList();
            return SelectedModels;
        }
    }
}
