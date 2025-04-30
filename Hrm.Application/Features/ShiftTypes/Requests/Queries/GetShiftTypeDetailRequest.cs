using Hrm.Application.DTOs.ShiftType;
using MediatR;


namespace Hrm.Application.Features.ShiftTypes.Requests.Queries
{
    public class GetShiftTypeDetailRequest : IRequest<ShiftTypeDto>
    {
        public int Id { get; set; }
    }
}
