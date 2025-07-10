using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.FinancialYears.Requests.Commands
{
    public class DeleteFinancialYearCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
