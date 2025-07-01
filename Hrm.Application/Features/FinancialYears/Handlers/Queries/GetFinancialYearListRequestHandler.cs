using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.FinancialYears.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.FinancialYear;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.FinancialYears.Handlers.Queries
{
    public class GetFinancialYearListRequestHandler : IRequestHandler<GetFinancialYearListRequest, object>
    {

        private readonly IHrmRepository<FinancialYear> _FinancialYearRepository;

        private readonly IMapper _mapper;

        public GetFinancialYearListRequestHandler(IHrmRepository<FinancialYear> FinancialYearRepository, IMapper mapper)
        {
            _FinancialYearRepository = FinancialYearRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetFinancialYearListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<FinancialYear> FinancialYears = _FinancialYearRepository.Where(x => true);

            FinancialYears = FinancialYears.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var FinancialYearsDtos = _mapper.Map<List<FinancialYearDto>>(FinancialYears);


            return FinancialYearsDtos;
        }
    }
}
