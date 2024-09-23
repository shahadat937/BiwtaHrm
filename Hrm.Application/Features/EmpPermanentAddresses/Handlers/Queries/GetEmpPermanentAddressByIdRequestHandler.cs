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
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.EmpPermanentAddress;

namespace Hrm.Application.Features.EmpPermanentAddresses.Handlers.Queries
{
    public class GetEmpPermanentAddressByIdRequestHandler : IRequestHandler<GetEmpPermanentAddressByIdRequest, object>
    {

        private readonly IHrmRepository<EmpPermanentAddress> _EmpPermanentAddressRepository;
        private readonly IMapper _mapper;
        public GetEmpPermanentAddressByIdRequestHandler(IHrmRepository<EmpPermanentAddress> EmpPermanentAddressRepository, IMapper mapper)
        {
            _EmpPermanentAddressRepository = EmpPermanentAddressRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPermanentAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPermanentAddress = await _EmpPermanentAddressRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Country)
                .Include(x => x.Division)
                .Include(x => x.District)
                .Include(x => x.Upazila)
                .Include(x => x.Thana)
                //.Include(x => x.Union)
                //.Include(x => x.Ward)
                .FirstOrDefaultAsync(cancellationToken);

            if (EmpPermanentAddress == null)
            {
                return null;
            }

            var EmpPermanentAddressDto = _mapper.Map<EmpPermanentAddressDto>(EmpPermanentAddress);

            return EmpPermanentAddressDto;
        }
    }
}