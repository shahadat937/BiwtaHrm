using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.ShiftType;
using Hrm.Application.Features.ShiftTypes.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftTypes.Handlers.Queries
{
    public class GetActiveShiftTypeRequestHandler : IRequestHandler<GetActiveShiftTypeRequest, object>
    {

        private readonly IHrmRepository<ShiftType> _ShiftTypeRepository;

        private readonly IMapper _mapper;

        public GetActiveShiftTypeRequestHandler(IHrmRepository<ShiftType> ShiftTypeRepository, IMapper mapper)
        {
            _ShiftTypeRepository = ShiftTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetActiveShiftTypeRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            var ShiftTypes = await _ShiftTypeRepository.FindOneAsync(x => x.IsActive == true);


            var ShiftTypesDtos = _mapper.Map<ShiftTypeDto>(ShiftTypes);


            return ShiftTypesDtos;
        }
    }
}
