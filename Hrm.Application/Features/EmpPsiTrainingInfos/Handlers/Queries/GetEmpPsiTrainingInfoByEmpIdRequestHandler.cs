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
            ICollection<EmpPsiTrainingInfo> empPsiTrainingInfos = await _EmpPsiTrainingInfoRepository.FilterAsync(x => x.EmpId == request.Id);

            List<EmpPsiTrainingInfoDto> result = _mapper.Map<List<EmpPsiTrainingInfoDto>>(empPsiTrainingInfos);

            return result;
        }
    }
}