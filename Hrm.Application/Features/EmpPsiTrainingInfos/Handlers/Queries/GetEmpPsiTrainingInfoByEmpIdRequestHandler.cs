using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPsiTrainingInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpPsiTrainingInfo;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpPsiTrainingInfos.Handlers.Queries
{
    public class GetEmpPsiTrainingInfoByEmpIdRequestHandler : IRequestHandler<GetEmpPsiTrainingInfoByEmpIdRequest, List<EmpPsiTrainingInfoDto>>
    {

        private readonly IHrmRepository<EmpPsiTrainingInfo> _EmpPsiTrainingInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpPsiTrainingInfoByEmpIdRequestHandler(IHrmRepository<EmpPsiTrainingInfo> EmpPsiTrainingInfoRepository, IMapper mapper)
        {
            _EmpPsiTrainingInfoRepository = EmpPsiTrainingInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpPsiTrainingInfoDto>> Handle(GetEmpPsiTrainingInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpPsiTrainingInfo> empPsiTrainingInfos = await _EmpPsiTrainingInfoRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.TrainingName)
                .ToListAsync(cancellationToken);

            if (empPsiTrainingInfos == null)
            {
                return null;
            }

            List<EmpPsiTrainingInfoDto> result = _mapper.Map<List<EmpPsiTrainingInfoDto>>(empPsiTrainingInfos);

            return result;
        }
    }
}