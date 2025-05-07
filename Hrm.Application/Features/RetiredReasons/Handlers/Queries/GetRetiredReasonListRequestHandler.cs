using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.RetiredReasons.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.RetiredReason;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.RetiredReasons.Handlers.Queries
{
    public class GetRetiredReasonListRequestHandler : IRequestHandler<GetRetiredReasonListRequest, object>
    {

        private readonly IHrmRepository<RetiredReason> _RetiredReasonRepository;

        private readonly IMapper _mapper;

        public GetRetiredReasonListRequestHandler(IHrmRepository<RetiredReason> RetiredReasonRepository, IMapper mapper)
        {
            _RetiredReasonRepository = RetiredReasonRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetRetiredReasonListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<RetiredReason> RetiredReasons = _RetiredReasonRepository.Where(x => true);

            RetiredReasons = RetiredReasons.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var RetiredReasonsDtos = _mapper.Map<List<RetiredReasonDto>>(RetiredReasons);


            return RetiredReasonsDtos;
        }
    }
}
