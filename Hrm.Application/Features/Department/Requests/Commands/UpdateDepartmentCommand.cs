using Hrm.Application.DTOs.Department;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.Department.Requests.Commands
{
    public class UpdateDepartmentCommand : IRequest<BaseCommandResponse>
    {
        public required DepartmentDto DepartmentDto { get; set; }
    }
}
