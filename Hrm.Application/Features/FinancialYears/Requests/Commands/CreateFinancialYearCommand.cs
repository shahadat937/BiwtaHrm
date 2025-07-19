using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.FinancialYear;


namespace Hrm.Application.Features.FinancialYears.Requests.Commands
{
    public class CreateFinancialYearCommand : IRequest<BaseCommandResponse> 
    {
        public CreateFinancialYearDto FinancialYearDto { get; set; }

    }
}
