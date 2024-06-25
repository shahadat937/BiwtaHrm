using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteSubDepartmentCommand : IRequest
    {
        public int SubDepartmentId { get; set; }
    }
}
