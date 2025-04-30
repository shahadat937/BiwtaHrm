using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.ShiftTypes.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.ShiftType;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.ShiftTypes.Handlers.Queries
{
    public class GetShiftTypeListRequestHandler : IRequestHandler<GetShiftTypeListRequest, object>
    {

        private readonly IHrmRepository<ShiftType> _ShiftTypeRepository;

        private readonly IMapper _mapper;

        public GetShiftTypeListRequestHandler(IHrmRepository<ShiftType> ShiftTypeRepository, IMapper mapper)
        {
            _ShiftTypeRepository = ShiftTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShiftTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<ShiftType> ShiftTypes = _ShiftTypeRepository.Where(x => true);

            ShiftTypes = ShiftTypes.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var ShiftTypesDtos = _mapper.Map<List<ShiftTypeDto>>(ShiftTypes);


            return ShiftTypesDtos;
        }
    }
}
