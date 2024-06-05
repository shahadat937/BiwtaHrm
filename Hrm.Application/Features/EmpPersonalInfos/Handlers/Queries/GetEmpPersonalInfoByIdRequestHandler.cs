using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPersonalInfos.Handlers.Queries
{
    public class GetEmpPersonalInfoByIdRequestHandler : IRequestHandler<GetEmpPersonalInfoByIdRequest, object>
    {

        private readonly IHrmRepository<EmpPersonalInfo> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpPersonalInfoByIdRequestHandler(IHrmRepository<EmpPersonalInfo> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPersonalInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPersonalInfo = _EmpPersonalInfoRepository.FinedOneInclude(x => x.EmpId == request.Id);

            return EmpPersonalInfo;
        }
    }
}