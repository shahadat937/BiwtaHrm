using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteEmployeeTypeCommand : IRequest
    {
        public int EmployeeTypeId { get; set; }
    }
}
