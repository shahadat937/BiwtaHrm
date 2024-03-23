using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Handlers.Queries
{

    public class GetScaleCommandHandler : IRequestHandler<GetScaleRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Scale> _ScaleRepository;
        private readonly IMapper _mapper;
        public GetScaleCommandHandler(IHrmRepository<Hrm.Domain.Scale> ScaleRepository, IMapper mapper)
        {
            _ScaleRepository = ScaleRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetScaleRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Scale> Scale = _ScaleRepository.Where(x => true);

            var ScaleDtos = await Task.Run(() => _mapper.Map<List<ScaleDto>>(Scale));

            return ScaleDtos;
        }
    }
}
