using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetEmpBasicInfoByAspNetUserIdRequestHandler : IRequestHandler<GetEmpBasicInfoByAspNetUserIdRequest, object>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpBasicInfoByAspNetUserIdRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpBasicInfoByAspNetUserIdRequest request, CancellationToken cancellationToken)
        {
            var EmpBasicInfo = _EmpBasicInfoRepository.FinedOneInclude(x => x.AspNetUserId == request.AspNetUserId);

            return EmpBasicInfo;
        }
    }
}
