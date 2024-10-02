using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Handlers.Queries
{
    public class GetScaleCommandHandler : IRequestHandler<GetScaleRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.Scale> _scaleRepository;
        private readonly IMapper _mapper;
        public GetScaleCommandHandler(IHrmRepository<Hrm.Domain.Scale> scaleRepository, IMapper mapper)
        {
            _scaleRepository = scaleRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetScaleRequest request, CancellationToken cancellationToken)
        {
            var Scale = await _scaleRepository.Where(x => true)
                .Include(x => x.Grade)
                .ToListAsync(cancellationToken);


            var BloodGroupDtos = _mapper.Map<List<ScaleDto>>(Scale);

            return BloodGroupDtos;
        }
    }

}
