using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana_Upazila;
using Hrm.Application.Features.Thana_Upazila.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana_Upazila.Handlers.Queries
{
    public class GetThana_UpazilaRequestHandler : IRequestHandler<GetThana_UpazilaRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Thana_Upazila> _Thana_UpazilaRepository;
        private readonly IMapper _mapper;
        public GetThana_UpazilaRequestHandler(IHrmRepository<Hrm.Domain.Thana_Upazila> Thana_UpazilaRepository, IMapper mapper)
        {
            _Thana_UpazilaRepository = Thana_UpazilaRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetThana_UpazilaRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.Thana_Upazila> Thana_Upazila = _Thana_UpazilaRepository.Where(x => true);

            //var Thana_UpazilaDtos = _mapper.Map<List<Thana_UpazilaDto>>(Thana_Upazila);

            //return Thana_UpazilaDtos;



            IQueryable<Hrm.Domain.Thana_Upazila> Thana_Upazila = _Thana_UpazilaRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var Thana_UpazilaDtos = await Task.Run(() => _mapper.Map<List<Thana_UpazilaDto>>(Thana_Upazila));

            return Thana_UpazilaDtos;
        }
    }
}
