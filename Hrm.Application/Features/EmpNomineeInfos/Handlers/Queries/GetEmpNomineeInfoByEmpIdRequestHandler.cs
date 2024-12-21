using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpNomineeInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpNomineeInfo;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpNomineeInfos.Handlers.Queries
{
    public class GetEmpNomineeInfoByEmpIdRequestHandler : IRequestHandler<GetEmpNomineeInfoByEmpIdRequest, List<EmpNomineeInfoDto>>
    {

        private readonly IHrmRepository<EmpNomineeInfo> _EmpNomineeInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpNomineeInfoByEmpIdRequestHandler(IHrmRepository<EmpNomineeInfo> EmpNomineeInfoRepository, IMapper mapper)
        {
            _EmpNomineeInfoRepository = EmpNomineeInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpNomineeInfoDto>> Handle(GetEmpNomineeInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<EmpNomineeInfo> EmpNomineeInfos = await _EmpNomineeInfoRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Relation).ToListAsync();

            List<EmpNomineeInfoDto> result = _mapper.Map<List<EmpNomineeInfoDto>>(EmpNomineeInfos);

            return result;
        }
    }
}