using Hrm.Application.DTOs.FinancialYear;
using MediatR;


namespace Hrm.Application.Features.FinancialYears.Requests.Queries
{
    public class GetFinancialYearDetailRequest : IRequest<FinancialYearDto>
    {
        public int Id { get; set; }
    }
}
