using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Application.Features.Upazila.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Queries
{
    public class GetThanaByUpazilaIdRequestHandler:IRequestHandler<GetThanaByUpazilaIdRequest,List<ThanaDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.Thana> _ThanaRepository;
        private readonly IMapper _mapper;
        public GetThanaByUpazilaIdRequestHandler(IHrmRepository<Hrm.Domain.Thana> ThanaRepositoy, IMapper mapper)
        {
            _ThanaRepository = ThanaRepositoy;
            _mapper = mapper;
        }
        public async Task<List<ThanaDto>> Handle(GetThanaByUpazilaIdRequest request, CancellationToken cancellationToken)
        {

            ICollection<Hrm.Domain.Thana> Thanas = _ThanaRepository.FilterWithInclude(x => x.UpazilaId == request.UpazilaId).ToList();

            var ThanasDtos = _mapper.Map<List<ThanaDto>>(Thanas);
            return ThanasDtos;
        }

    }
}
