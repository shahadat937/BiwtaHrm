using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeBranch.Handlers.Queries
{
    public class GetBranchByOfficeIdRequestHandler : IRequestHandler<GetBranchByOfficeIdRequest, List<SelectedModel>>
    {

        private readonly IHrmRepository<Hrm.Domain.OfficeBranch> _OfficeBranchRepository;
        private readonly IMapper _mapper;
        public GetBranchByOfficeIdRequestHandler(IHrmRepository<Hrm.Domain.OfficeBranch> OfficeBranchRepository, IMapper mapper)
        {
            _OfficeBranchRepository = OfficeBranchRepository;
            _mapper = mapper;

        }

        public async Task<List<SelectedModel>> Handle(GetBranchByOfficeIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.OfficeBranch> branches = _OfficeBranchRepository.FilterWithInclude(x => x.OfficeId == request.OfficeId).ToList();
            List<SelectedModel> SelectedModel = branches.Select(x => new SelectedModel
            {
                Id = x.BranchId,
                Name = x.BranchName
            }).ToList();
            return SelectedModel;
        }
    }
}
