using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpJobDetails.Handlers.Queries
{
    public class GetEmpJobDetailByIdRequestHandler : IRequestHandler<GetEmpJobDetailByIdRequest, object>
    {

        private readonly IHrmRepository<EmpJobDetail> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpJobDetailByIdRequestHandler(IHrmRepository<EmpJobDetail> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpJobDetailByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpJobDetail = _EmpPersonalInfoRepository.FinedOneInclude(x => x.EmpId == request.Id);

            return EmpJobDetail;
        }
    }
}