using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Queries
{
    public class GetUpazilaRequestHandler : IRequestHandler<GetUpazilaRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IMapper _mapper;
        public GetUpazilaRequestHandler(IHrmRepository<Hrm.Domain.Upazila> UpazilaRepository, IMapper mapper)
        {
            _UpazilaRepository = UpazilaRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetUpazilaRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.Upazila> Upazila = _UpazilaRepository.Where(x => true);

            //var UpazilaDtos = _mapper.Map<List<UpazilaDto>>(Upazila);

            //return UpazilaDtos;



            IQueryable<Hrm.Domain.Upazila> Upazila = _UpazilaRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var UpazilaDtos = await Task.Run(() => _mapper.Map<List<UpazilaDto>>(Upazila));

            return UpazilaDtos;
        }
    }
}
