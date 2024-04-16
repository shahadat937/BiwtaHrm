using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteOverall_EV_PromotionCommand : IRequest
    {
        public int Overall_EV_PromotionId { get; set; }
    }
}
