using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.DayType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DayType.Handlers.Queries
{
    public class GetSelectedDayTypeRequestHandler: IRequestHandler<GetSelectedDayTypeRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.DayType> _DayTypeRepository;
        private readonly IMapper _mapper;

        public GetSelectedDayTypeRequestHandler (IHrmRepository<Domain.DayType> dayTypeRepository, IMapper mapper)
        {
            _DayTypeRepository = dayTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSelectedDayTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.DayType> daytype = await _DayTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = daytype.Select(x => new SelectedModel
            {
                Name = x.DayTypeName,
                Id = x.DayTypeId
            }).ToList();
            return selectModels;
        }
    }
}
