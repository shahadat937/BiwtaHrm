using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.Division;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.Division.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Handlers.Queries
{
    public class GetDistrictByDivisionIdRequestHandler:IRequestHandler<GetDistrictByDivisionIdRequest, List<DistrictDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.District> _DistrictRepository;
        private readonly IMapper _mapper;
        public GetDistrictByDivisionIdRequestHandler(IHrmRepository<Hrm.Domain.District> DistrictRepositoy, IMapper mapper)
        {
            _DistrictRepository = DistrictRepositoy;
            _mapper = mapper;
        }

        public async Task<List<DistrictDto>> Handle(GetDistrictByDivisionIdRequest request, CancellationToken cancellationToken)
        {
            
            ICollection<Hrm.Domain.District> Districts = _DistrictRepository.FilterWithInclude(x => x.DivisionId == request.DivisionId).ToList();
         
            var DistrictsDtos = _mapper.Map<List<DistrictDto>>(Districts);
            return DistrictsDtos;
        }
    }
}
