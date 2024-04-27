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
    public class GetOfficeAddressRequestHandler : IRequestHandler<GetOfficeAddressRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.OfficeAddress> _OfficeAddressRepository;
        private readonly IMapper _mapper;
        public GetOfficeAddressRequestHandler(IHrmRepository<Hrm.Domain.OfficeAddress> OfficeAddressRepository, IMapper mapper)
        {
            _OfficeAddressRepository = OfficeAddressRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetOfficeAddressRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.OfficeAddress> OfficeAddresss = _OfficeAddressRepository.Where(x => true);

            var OfficeAddressDtos = _mapper.Map<List<OfficeAddressDto>>(OfficeAddresss);

            return OfficeAddressDtos;
        }
    }
}
