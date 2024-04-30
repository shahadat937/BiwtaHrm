using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.Union.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Handlers.Queries
{
    public class GetUnionByThanaIdRequestHandler:IRequestHandler<GetUnionByThanaIdRequest,List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Union> _UnionRepository;
        private readonly IMapper _mapper;
        public GetUnionByThanaIdRequestHandler(IHrmRepository<Hrm.Domain.Union> UnionRepositoy, IMapper mapper)
        {
            _UnionRepository = UnionRepositoy;
            _mapper = mapper;
        }

        public async Task<List<SelectedModel>> Handle(GetUnionByThanaIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Union> Thanas = _UnionRepository.FilterWithInclude(x => x.ThanaId == request.ThanaId).ToList();
            List<SelectedModel> SelectedModels = Thanas.Select(x => new SelectedModel
            {
                Id = x.UnionId,
                Name = x.UnionName
            }).ToList();
            return SelectedModels;
        }
    }
}
