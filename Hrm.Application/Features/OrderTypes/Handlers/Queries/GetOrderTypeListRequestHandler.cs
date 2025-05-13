using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.OrderTypes.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.OrderType;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.OrderTypes.Handlers.Queries
{
    public class GetOrderTypeListRequestHandler : IRequestHandler<GetOrderTypeListRequest, object>
    {

        private readonly IHrmRepository<OrderType> _OrderTypeRepository;

        private readonly IMapper _mapper;

        public GetOrderTypeListRequestHandler(IHrmRepository<OrderType> OrderTypeRepository, IMapper mapper)
        {
            _OrderTypeRepository = OrderTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetOrderTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<OrderType> OrderTypes = _OrderTypeRepository.Where(x => true);

            OrderTypes = OrderTypes.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var OrderTypesDtos = _mapper.Map<List<OrderTypeDto>>(OrderTypes);


            return OrderTypesDtos;
        }
    }
}
