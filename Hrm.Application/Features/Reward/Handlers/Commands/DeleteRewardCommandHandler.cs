using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Rewards.Handlers.Commands
{
    public class DeleteRewardCommandHandler : IRequestHandler<DeleteRewardCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRewardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteRewardCommand request, CancellationToken cancellationToken)
        {
            var Reward = await _unitOfWork.Repository<Reward>().Get(request.RewardId);

            if (Reward == null)
                throw new NotFoundException(nameof(Reward), request.RewardId);

            await _unitOfWork.Repository<Reward>().Delete(Reward);
            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            //await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
