using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpPresentAddress;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpPresentAddresses.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPresentAddresses.Handlers.Queries
{
    public class GetEmpPresentAddressByIdRequestHandler : IRequestHandler<GetEmpPresentAddressByIdRequest, object>
    {

        private readonly IHrmRepository<EmpPresentAddress> _EmpPresentAddressRepository;
        private readonly IMapper _mapper;
        public GetEmpPresentAddressByIdRequestHandler(IHrmRepository<EmpPresentAddress> EmpPresentAddressRepository, IMapper mapper)
        {
            _EmpPresentAddressRepository = EmpPresentAddressRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPresentAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var empPresentAddress = await _EmpPresentAddressRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Country)
                .Include(x => x.Division)
                .Include(x => x.District)
                .Include(x => x.Upazila)
                .Include(x => x.Thana)
                //.Include(x => x.Union)
                //.Include(x => x.Ward)
                .FirstOrDefaultAsync(cancellationToken);

            if (empPresentAddress == null)
            {
                return null;
            }

            var empPresentAddressDto = _mapper.Map<EmpPresentAddressDto>(empPresentAddress);

            return empPresentAddressDto;
        }
    }
}