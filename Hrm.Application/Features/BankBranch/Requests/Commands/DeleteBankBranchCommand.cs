using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteBankBranchCommand : IRequest
    {
        public int BankBranchId { get; set; }
    }
}
