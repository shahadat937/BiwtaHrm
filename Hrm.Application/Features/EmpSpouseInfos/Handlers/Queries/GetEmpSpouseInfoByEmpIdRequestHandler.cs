using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpSpouseInfos.Handlers.Queries
{
    public class GetEmpSpouseInfoByEmpIdRequestHandler : IRequestHandler<GetEmpSpouseInfoByEmpIdRequest, object>
    {

        private readonly IHrmRepository<EmpSpouseInfo> _EmpSpouseInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpSpouseInfoByEmpIdRequestHandler(IHrmRepository<EmpSpouseInfo> EmpSpouseInfoRepository, IMapper mapper)
        {
            _EmpSpouseInfoRepository = EmpSpouseInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpSpouseInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var EmpJobDetail = _EmpSpouseInfoRepository.FinedOneInclude(x => x.EmpId == request.Id);

            return EmpJobDetail;
        }
    }
}