using Hrm.Application.DTOs.RetiredReason;
using MediatR;


namespace Hrm.Application.Features.RetiredReasons.Requests.Queries
{
    public class GetRetiredReasonDetailRequest : IRequest<RetiredReasonDto>
    {
        public int Id { get; set; }
    }
}
