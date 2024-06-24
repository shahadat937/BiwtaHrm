using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpPermanentAddresses.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPermanentAddresses.Handlers.Queries
{
    public class GetEmpPermanentAddressByIdRequestHandler : IRequestHandler<GetEmpPermanentAddressByIdRequest, object>
    {

        private readonly IHrmRepository<EmpPermanentAddress> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpPermanentAddressByIdRequestHandler(IHrmRepository<EmpPermanentAddress> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPermanentAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPermanentAddress = _EmpPersonalInfoRepository.FinedOneInclude(x => x.EmpId == request.Id);

            return EmpPermanentAddress;
        }
    }
}