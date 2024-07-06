using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpChildInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpChildInfo;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpChildInfos.Handlers.Queries
{
    public class GetEmpChildInfoByEmpIdRequestHandler : IRequestHandler<GetEmpChildInfoByEmpIdRequest, List<EmpChildInfoDto>>
    {

        private readonly IHrmRepository<EmpChildInfo> _EmpChildInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpChildInfoByEmpIdRequestHandler(IHrmRepository<EmpChildInfo> EmpChildInfoRepository, IMapper mapper)
        {
            _EmpChildInfoRepository = EmpChildInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpChildInfoDto>> Handle(GetEmpChildInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpChildInfo> empChildInfos = await _EmpChildInfoRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Occupation)
                .Include(x => x.Gender)
                .Include(x => x.MaritalStatus)
                .Include(x => x.ChildStatus)
                .ToListAsync(cancellationToken);

            if (empChildInfos == null)
            {
                return null;
            }

            List<EmpChildInfoDto> result = _mapper.Map<List<EmpChildInfoDto>>(empChildInfos);

            return result;
        }
    }
}