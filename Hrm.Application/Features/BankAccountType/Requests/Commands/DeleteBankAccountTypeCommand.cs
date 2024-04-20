using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteBankAccountTypeCommand : IRequest
    {
        public int BankAccountTypeId { get; set; }
    }
}
