using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Religion.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using SchoolManagement.Application.DTOs.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Religion.Handlers.Queries
{
    public class GetReligionRequestHandler : IRequestHandler<GetReligionRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Religion> _ReligionRepository;
        private readonly IMapper _mapper;
        public GetReligionRequestHandler(IHrmRepository<Hrm.Domain.Religion> ReligionRepository, IMapper mapper)
        {
            _ReligionRepository = ReligionRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetReligionRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Religion> Religion = _ReligionRepository.Where(x => true);

            var ReligionDtos = _mapper.Map<List<ReligionDto>>(Religion);

            return ReligionDtos;
        }
    }
}