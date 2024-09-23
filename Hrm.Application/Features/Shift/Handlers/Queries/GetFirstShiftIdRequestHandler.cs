using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Shift.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Shift.Handlers.Queries
{
    public class GetFirstShiftIdRequestHandler : IRequestHandler<GetFirstShiftIdRequest, int>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Shift> _ShiftRepository;
        public GetFirstShiftIdRequestHandler(IHrmRepository<Hrm.Domain.Shift> ShiftRepository, IMapper mapper)
        {
            _ShiftRepository = ShiftRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(GetFirstShiftIdRequest request, CancellationToken cancellationToken)
        {
            var ShiftId = await _ShiftRepository.FindOneAsync(x => x.IsActive == true);
            return ShiftId.ShiftId;
        }
    }
}
