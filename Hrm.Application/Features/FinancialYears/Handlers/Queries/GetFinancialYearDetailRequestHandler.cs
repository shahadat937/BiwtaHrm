using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FinancialYear;
using Hrm.Application.Features.FinancialYears.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.FinancialYears.Handlers.Queries
{
    public class GetFinancialYearsDetailRequestHandler : IRequestHandler<GetFinancialYearDetailRequest, FinancialYearDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<FinancialYear> _FinancialYearRepository;
        public GetFinancialYearsDetailRequestHandler(IHrmRepository<FinancialYear> FinancialYearRepository, IMapper mapper)
        {
            _FinancialYearRepository = FinancialYearRepository;
            _mapper = mapper;
        }
        public async Task<FinancialYearDto> Handle(GetFinancialYearDetailRequest request, CancellationToken cancellationToken)
        {
            var FinancialYear = await _FinancialYearRepository.Get(request.Id);
            return _mapper.Map<FinancialYearDto>(FinancialYear);
        }
    }
}
