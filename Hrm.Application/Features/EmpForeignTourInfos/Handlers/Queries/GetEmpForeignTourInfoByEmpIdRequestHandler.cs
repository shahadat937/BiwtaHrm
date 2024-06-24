using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpForeignTourInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpForeignTourInfo;

namespace Hrm.Application.Features.EmpForeignTourInfos.Handlers.Queries
{
    public class GetEmpForeignTourInfoByEmpIdRequestHandler : IRequestHandler<GetEmpForeignTourInfoByEmpIdRequest, List<EmpForeignTourInfoDto>>
    {

        private readonly IHrmRepository<EmpForeignTourInfo> _EmpForeignTourInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpForeignTourInfoByEmpIdRequestHandler(IHrmRepository<EmpForeignTourInfo> EmpForeignTourInfoRepository, IMapper mapper)
        {
            _EmpForeignTourInfoRepository = EmpForeignTourInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpForeignTourInfoDto>> Handle(GetEmpForeignTourInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<EmpForeignTourInfo> empForeignTourInfos = await _EmpForeignTourInfoRepository.FilterAsync(x => x.EmpId == request.Id);

            List<EmpForeignTourInfoDto> result = _mapper.Map<List<EmpForeignTourInfoDto>>(empForeignTourInfos);

            return result;
        }
    }
}