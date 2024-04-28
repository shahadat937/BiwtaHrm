using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.Upazila.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Queries
{
    public class GetUpazilaByDistrictIdRequestHandler:IRequestHandler<GetUpazilaByDistrictIdRequest,List<UpazilaDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IMapper _mapper;
        public GetUpazilaByDistrictIdRequestHandler(IHrmRepository<Hrm.Domain.Upazila> UpazilaRepositoy, IMapper mapper)
        {
            _UpazilaRepository = UpazilaRepositoy;
            _mapper = mapper;
        }
        public async Task<List<UpazilaDto>> Handle(GetUpazilaByDistrictIdRequest request, CancellationToken cancellationToken)
        {

            ICollection<Hrm.Domain.Upazila> Upazilas =  _UpazilaRepository.FilterWithInclude(x => x.DistrictId == request.DistrictId).ToList();

            var UpazilasDtos = _mapper.Map<List<UpazilaDto>>(Upazilas);
            return UpazilasDtos;
        }
    }
}
