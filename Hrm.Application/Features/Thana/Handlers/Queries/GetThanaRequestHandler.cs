using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Queries
{
    public class GetThanaRequestHandler : IRequestHandler<GetThanaRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Thana> _ThanaRepository;
        private readonly IMapper _mapper;
        public GetThanaRequestHandler(IHrmRepository<Hrm.Domain.Thana> ThanaRepository, IMapper mapper)
        {
            _ThanaRepository = ThanaRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetThanaRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.Thana> Thana = _ThanaRepository.Where(x => true);

            //var ThanaDtos = _mapper.Map<List<ThanaDto>>(Thana);

            //return ThanaDtos;



            IQueryable<Hrm.Domain.Thana> Thana = _ThanaRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var ThanaDtos = await Task.Run(() => _mapper.Map<List<ThanaDto>>(Thana));

            return ThanaDtos;
        }
    }
}
