using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Year;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain;

namespace Hrm.Application.Features.Year.Handlers.Queries
{
    public class GetYearRequestHandler : IRequestHandler<GetYearRequest, object>
    {

        private readonly IHrmRepository<Domain.Year> _YearRepository;
        private readonly IMapper _mapper;
        public GetYearRequestHandler(IHrmRepository<Hrm.Domain.Year> YearRepository, IMapper mapper)
        {
            _YearRepository = YearRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetYearRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Year> Years = _YearRepository.Where(x => true);

            Years = Years.OrderByDescending(x => x.YearId);

            var YearDtos = _mapper.Map<List<YearDto>>(Years.ToList());

            return YearDtos;
        }
    }
}
