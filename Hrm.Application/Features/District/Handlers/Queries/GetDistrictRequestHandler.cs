using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Handlers.Queries
{
    public class GetDistrictRequestHandler : IRequestHandler<GetDistrictRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.District> _DistrictRepository;
        private readonly IMapper _mapper;
        public GetDistrictRequestHandler(IHrmRepository<Hrm.Domain.District> DistrictRepository, IMapper mapper)
        {
            _DistrictRepository = DistrictRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDistrictRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.District> Districts = _DistrictRepository.Where(x => true);

            var DistrictDtos = _mapper.Map<List<DistrictDto>>(Districts);

            return DistrictDtos;
        }
    }
}
