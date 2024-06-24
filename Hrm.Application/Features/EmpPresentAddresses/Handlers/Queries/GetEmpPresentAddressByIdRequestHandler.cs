using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpPresentAddresses.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPresentAddresses.Handlers.Queries
{
    public class GetEmpPresentAddressByIdRequestHandler : IRequestHandler<GetEmpPresentAddressByIdRequest, object>
    {

        private readonly IHrmRepository<EmpPresentAddress> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpPresentAddressByIdRequestHandler(IHrmRepository<EmpPresentAddress> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPresentAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPresentAddress = _EmpPersonalInfoRepository.FinedOneInclude(x => x.EmpId == request.Id);

            return EmpPresentAddress;
        }
    }
}