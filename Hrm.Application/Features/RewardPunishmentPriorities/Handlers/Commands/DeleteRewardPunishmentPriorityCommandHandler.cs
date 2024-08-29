using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentPriority.Handlers.Commands
{
    public class DeleteRewardPunishmentPriorityCommandHandler : IRequestHandler<DeleteRewardPunishmentPriorityCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRewardPunishmentPriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteRewardPunishmentPriorityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var RewardPunishmentPriority = await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentPriority>().Get(request.RewardPunishmentPriorityId);

            if (RewardPunishmentPriority == null)
            {
                throw new NotFoundException(nameof(RewardPunishmentPriority), request.RewardPunishmentPriorityId);
            }

            await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentPriority>().Delete(RewardPunishmentPriority);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = RewardPunishmentPriority.Id;

            return response;
        }
    }
}
