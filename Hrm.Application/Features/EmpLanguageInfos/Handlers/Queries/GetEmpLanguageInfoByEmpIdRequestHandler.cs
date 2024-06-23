using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpLanguageInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpLanguageInfo;

namespace Hrm.Application.Features.EmpLanguageInfos.Handlers.Queries
{
    public class GetEmpLanguageInfoByEmpIdRequestHandler : IRequestHandler<GetEmpLanguageInfoByEmpIdRequest, List<EmpLanguageInfoDto>>
    {

        private readonly IHrmRepository<EmpLanguageInfo> _EmpLanguageInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpLanguageInfoByEmpIdRequestHandler(IHrmRepository<EmpLanguageInfo> EmpLanguageInfoRepository, IMapper mapper)
        {
            _EmpLanguageInfoRepository = EmpLanguageInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpLanguageInfoDto>> Handle(GetEmpLanguageInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<EmpLanguageInfo> empLanguageInfos = await _EmpLanguageInfoRepository.FilterAsync(x => x.EmpId == request.Id);

            List<EmpLanguageInfoDto> result = _mapper.Map<List<EmpLanguageInfoDto>>(empLanguageInfos);

            return result;
        }
    }
}