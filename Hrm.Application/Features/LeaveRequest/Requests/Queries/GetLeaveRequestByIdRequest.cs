using MediatR;
using Hrm.Application.DTOs.LeaveRequest;
namespace Hrm.Application;

public class GetLeaveRequestByIdRequest: IRequest<LeaveRequestDto>
{
    public int LeaveRequestId {get; set;}
}
