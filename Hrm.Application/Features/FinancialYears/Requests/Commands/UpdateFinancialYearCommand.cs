using Hrm.Application.DTOs.FinancialYear;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.FinancialYears.Requests.Commands
{
    public class UpdateFinancialYearCommand : IRequest<BaseCommandResponse>  
    {
        public CreateFinancialYearDto FinancialYearDto { get; set; }
    }
}
