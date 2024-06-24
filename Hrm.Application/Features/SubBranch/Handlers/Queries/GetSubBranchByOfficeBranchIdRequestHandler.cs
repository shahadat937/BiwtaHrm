using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.Features.SubBranch.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Handlers.Queries
{
    public class GetDiviosionByOfficeBranchIdRequestHandler : IRequestHandler<GetSubBranchByOfficeBranchIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository;
        private readonly IMapper _mapper;
        public GetDiviosionByOfficeBranchIdRequestHandler(IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepositoy, IMapper mapper)
        {
            _SubBranchRepository = SubBranchRepositoy;
            _mapper = mapper;

        }
        public async Task<List<SelectedModel>> Handle(GetSubBranchByOfficeBranchIdRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.SubBranch> SubBranchs = _SubBranchRepository.Get(request.CountryId);
            //var SubBranchDtos = new List<SubBranchDto>();
            //return SubBranchDtos;
            ICollection<Hrm.Domain.SubBranch> SubBranchs = _SubBranchRepository.FilterWithInclude(x => x.BranchId == request.OfficeBranchId).ToList();
            List<SelectedModel> SubBranchNames = SubBranchs.Select(x => new SelectedModel
            {
                Id = x.SubBranchId,
                Name = x.SubBranchName
            }).ToList();
         //   var SubBranchDtos = _mapper.Map<List<SubBranchDto>>(SubBranchNames);
            return SubBranchNames;
        }
    }
}
