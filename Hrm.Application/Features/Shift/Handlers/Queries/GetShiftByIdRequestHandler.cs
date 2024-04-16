using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Shift.Handlers.Queries
{
    public class GetShiftByIdRequestHandler : IRequestHandler<GetShiftByIdRequest, ShiftDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Shift> _ShiftRepository;
        private readonly IMapper _mapper;
        public GetShiftByIdRequestHandler(IHrmRepository<Hrm.Domain.Shift> ShiftRepositoy, IMapper mapper)
        {
            _ShiftRepository = ShiftRepositoy;
            _mapper = mapper;
        }

        public async Task<ShiftDto> Handle(GetShiftByIdRequest request, CancellationToken cancellationToken)
        {
            var Shift = await _ShiftRepository.Get(request.ShiftId);
            return _mapper.Map<ShiftDto>(Shift);
        }
    }
}
