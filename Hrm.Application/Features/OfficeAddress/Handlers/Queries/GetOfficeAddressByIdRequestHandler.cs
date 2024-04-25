using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.OfficeAddress;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.OfficeAddress.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeAddress.Handlers.Queries
{
    public class GetOfficeAddressByIdRequestHandler : IRequestHandler<GetOfficeAddressByIdRequest, OfficeAddressDto>
    {

        private readonly IHrmRepository<Hrm.Domain.OfficeAddress> _OfficeAddressRepository;
        private readonly IMapper _mapper;
        public GetOfficeAddressByIdRequestHandler(IHrmRepository<Hrm.Domain.OfficeAddress> OfficeAddressRepositoy, IMapper mapper)
        {
            _OfficeAddressRepository = OfficeAddressRepositoy;
            _mapper = mapper;
        }

        public async Task<OfficeAddressDto> Handle(GetOfficeAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var OfficeAddress = await _OfficeAddressRepository.Get(request.OfficeAddressId);
            return _mapper.Map<OfficeAddressDto>(OfficeAddress);
        }
    }
}
