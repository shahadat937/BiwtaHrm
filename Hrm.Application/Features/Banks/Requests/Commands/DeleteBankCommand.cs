using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteBankCommand : IRequest
    {
        public int BankId { get; set; }
    }
}
