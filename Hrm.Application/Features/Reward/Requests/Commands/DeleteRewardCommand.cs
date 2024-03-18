using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteRewardCommand : IRequest
    {
        public int RewardId { get; set; }
    }
}
