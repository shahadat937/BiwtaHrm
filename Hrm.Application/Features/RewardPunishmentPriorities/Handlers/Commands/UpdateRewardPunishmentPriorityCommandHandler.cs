using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentPriority.Handlers.Commands
{
    public class UpdateRewardPunishmentPriorityCommandHandler : IRequestHandler<UpdateRewardPunishmentPriorityCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.RewardPunishmentPriority> _RewardPunishmentPriorityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRewardPunishmentPriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.RewardPunishmentPriority> RewardPunishmentPriorityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RewardPunishmentPriorityRepository = RewardPunishmentPriorityRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateRewardPunishmentPriorityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            IQueryable<Hrm.Domain.RewardPunishmentPriority> RewardPunishmentPrioritys = _RewardPunishmentPriorityRepository.Where(x => x.Name == request.RewardPunishmentPriorityDto.Name && x.Id != request.RewardPunishmentPriorityDto.Id);



            if (RewardPunishmentPrioritys.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.RewardPunishmentPriorityDto.Name}' already exists.";
            }

            else
            {

                var RewardPunishmentPriority = await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentPriority>().Get(request.RewardPunishmentPriorityDto.Id);

                if (RewardPunishmentPriority is null)
                {
                    response.Success = false;
                    response.Message = $"Update Failed '{request.RewardPunishmentPriorityDto.Id}' not found.";
                }

                _mapper.Map(request.RewardPunishmentPriorityDto, RewardPunishmentPriority);

                await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentPriority>().Update(RewardPunishmentPriority);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = RewardPunishmentPriority.Id;

            }

            return response;
        }
    }
}
