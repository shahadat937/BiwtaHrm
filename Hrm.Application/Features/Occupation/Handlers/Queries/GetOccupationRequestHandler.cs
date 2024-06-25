using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Occupation;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Occupation.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Occupation.Handlers.Queries
{
    public class GetOccupationRequestHandler : IRequestHandler<GetOccupationRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Occupation> _OccupationRepository;
        private readonly IMapper _mapper;
        public GetOccupationRequestHandler(IHrmRepository<Hrm.Domain.Occupation> OccupationRepository, IMapper mapper)
        {
            _OccupationRepository = OccupationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetOccupationRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.Occupation> Occupations = _OccupationRepository.Where(x => true);

            // Order blood groups by descending order
            Occupations = Occupations.OrderByDescending(x => x.OccupationId);

            // Map the ordered blood groups to OccupationDto
            var OccupationDtos = _mapper.Map<List<OccupationDto>>(Occupations.ToList());

            return OccupationDtos;
        }
    }
}
