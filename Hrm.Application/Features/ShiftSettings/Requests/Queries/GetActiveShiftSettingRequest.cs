using MediatR;
using Hrm.Application.DTOs.Common;
using Hrm.Application.Models;

namespace Hrm.Application.Features.ShiftSettings.Requests.Queries
{
    public class GetActiveShiftSettingRequest : IRequest<object>
    {
        public int ShiftTypeId { get; set; }
    }
}
