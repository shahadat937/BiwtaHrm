using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpEducationInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpEducationInfo;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpEducationInfos.Handlers.Queries
{
    public class GetEmpEducationInfoByEmpIdRequestHandler : IRequestHandler<GetEmpEducationInfoByEmpIdRequest, List<EmpEducationInfoDto>>
    {

        private readonly IHrmRepository<EmpEducationInfo> _EmpEducationInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpEducationInfoByEmpIdRequestHandler(IHrmRepository<EmpEducationInfo> EmpEducationInfoRepository, IMapper mapper)
        {
            _EmpEducationInfoRepository = EmpEducationInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpEducationInfoDto>> Handle(GetEmpEducationInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpEducationInfo> empEducationInfos = await _EmpEducationInfoRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.ExamType)
                .Include(x => x.Board)
                .Include(x => x.SubGroup)
                .ToListAsync(cancellationToken);

            if (empEducationInfos == null)
            {
                return null;
            }

            List<EmpEducationInfoDto> result = _mapper.Map<List<EmpEducationInfoDto>>(empEducationInfos);

            return result;
        }
    }
}