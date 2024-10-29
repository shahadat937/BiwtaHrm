using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpTrainingInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpTrainingInfo;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpTrainingInfos.Handlers.Queries
{
    public class GetEmpTrainingInfoByEmpIdRequestHandler : IRequestHandler<GetEmpTrainingInfoByEmpIdRequest, List<EmpTrainingInfoDto>>
    {

        private readonly IHrmRepository<EmpTrainingInfo> _EmpTrainingInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpTrainingInfoByEmpIdRequestHandler(IHrmRepository<EmpTrainingInfo> EmpTrainingInfoRepository, IMapper mapper)
        {
            _EmpTrainingInfoRepository = EmpTrainingInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpTrainingInfoDto>> Handle(GetEmpTrainingInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpTrainingInfo> EmpTrainingInfos = await _EmpTrainingInfoRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.TrainingType)
                .Include(x => x.Country)
                .ToListAsync(cancellationToken);

            if (EmpTrainingInfos == null)
            {
                return null;
            }

            List<EmpTrainingInfoDto> result = _mapper.Map<List<EmpTrainingInfoDto>>(EmpTrainingInfos);

            return result;
        }
    }
}