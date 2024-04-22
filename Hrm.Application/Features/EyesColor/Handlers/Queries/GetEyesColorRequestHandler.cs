using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EyesColor;
using Hrm.Application.DTOs.EyesColor;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EyesColor.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EyesColor.Handlers.Queries
{
    public class GetEyesColorRequestHandler : IRequestHandler<GetEyesColorRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EyesColor> _EyesColorRepository;
        private readonly IMapper _mapper;
        public GetEyesColorRequestHandler(IHrmRepository<Hrm.Domain.EyesColor> EyesColorRepository, IMapper mapper)
        {
            _EyesColorRepository = EyesColorRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEyesColorRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.EyesColor> EyesColor = _EyesColorRepository.Where(x => true);
            EyesColor = EyesColor.OrderByDescending(x => x.EyesColorId);

            var EyesColorDtos = _mapper.Map<List<EyesColorDto>>(EyesColor);

            return EyesColorDtos;
        }
    }
}