using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.CourseDuration;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.CourseDurations.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain;

namespace Hrm.Application.Features.CourseDurations.Handlers.Queries
{
    public class GetCourseDurationRequestHandler : IRequestHandler<GetCourseDurationRequest, object>
    {

        private readonly IHrmRepository<Domain.CourseDuration> _CourseDurationRepository;
        private readonly IMapper _mapper;
        public GetCourseDurationRequestHandler(IHrmRepository<Hrm.Domain.CourseDuration> CourseDurationRepository, IMapper mapper)
        {
            _CourseDurationRepository = CourseDurationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetCourseDurationRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.CourseDuration> CourseDurations = _CourseDurationRepository.Where(x => true);

            CourseDurations = CourseDurations.OrderByDescending(x => x.Id);

            var CourseDurationDtos = _mapper.Map<List<CourseDurationDto>>(CourseDurations.ToList());

            return CourseDurationDtos;
        }
    }
}
