using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Shift.Handlers.Queries
{
    public class GetShiftRequestHandler : IRequestHandler<GetShiftRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Shift> _ShiftRepository;
        private readonly IMapper _mapper;
        public GetShiftRequestHandler(IHrmRepository<Hrm.Domain.Shift> ShiftRepository, IMapper mapper)
        {
            _ShiftRepository = ShiftRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShiftRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Shift> Shift = _ShiftRepository.Where(x => true);

            var ShiftDtos = _mapper.Map<List<ShiftDto>>(Shift);

            return ShiftDtos;
        }
    }
}