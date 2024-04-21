using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HairColor;
using Hrm.Application.DTOs.HairColor;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.HairColor.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HairColor.Handlers.Queries
{
    public class GetHairColorRequestHandler : IRequestHandler<GetHairColorRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.HairColor> _HairColorRepository;
        private readonly IMapper _mapper;
        public GetHairColorRequestHandler(IHrmRepository<Hrm.Domain.HairColor> HairColorRepository, IMapper mapper)
        {
            _HairColorRepository = HairColorRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetHairColorRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.HairColor> HairColor = _HairColorRepository.Where(x => true);
            HairColor = HairColor.OrderByDescending(x => x.HairColorId);

            var HairColorDtos = _mapper.Map<List<HairColorDto>>(HairColor);

            return HairColorDtos;
        }
    }
}