using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentType.Handlers.Commands
{
    public class DeleteRewardPunishmentTypeCommandHandler : IRequestHandler<DeleteRewardPunishmentTypeCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRewardPunishmentTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteRewardPunishmentTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var RewardPunishmentType = await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentType>().Get(request.RewardPunishmentTypeId);

            if (RewardPunishmentType == null)
            {
                throw new NotFoundException(nameof(RewardPunishmentType), request.RewardPunishmentTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentType>().Delete(RewardPunishmentType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = RewardPunishmentType.Id;

            return response;
        }
    }
}
