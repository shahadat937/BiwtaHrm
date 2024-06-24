using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.SubBranch.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Handlers.Queries
{
    public class GetSubBranchRequestHandler : IRequestHandler<GetSubBranchRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository;
        private readonly IHrmRepository<Hrm.Domain.OfficeBranch> _BranchRepository;
        private readonly IMapper _mapper;
        public GetSubBranchRequestHandler(IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepository, IMapper mapper, IHrmRepository<Domain.OfficeBranch> BranchRepository)
        {
            _SubBranchRepository = SubBranchRepository;
            _mapper = mapper;
            _BranchRepository = BranchRepository;
        }

        public async Task<object> Handle(GetSubBranchRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.SubBranch> SubBranchs = _SubBranchRepository.FilterWithInclude(x => true);
            SubBranchs = SubBranchs.OrderByDescending(x => x.SubBranchId);

            var SubBranchDtos = new List<SubBranchDto>();

            foreach (var SubBranch in SubBranchs)
            {
                var SubBranchDto = _mapper.Map<SubBranchDto>(SubBranch);
                var BranchName = await GetBranchName(SubBranch.BranchId);
                SubBranchDto.BranchName = BranchName;
                SubBranchDtos.Add(SubBranchDto);
            }

            return SubBranchDtos;
        }

        private async Task<string?> GetBranchName(int? BranchId)
        {
            if (BranchId.HasValue)
            {
                var Branch = await _BranchRepository.Get(BranchId.Value);
                return Branch?.BranchName;
            }
            return null;
        }
    }
}
