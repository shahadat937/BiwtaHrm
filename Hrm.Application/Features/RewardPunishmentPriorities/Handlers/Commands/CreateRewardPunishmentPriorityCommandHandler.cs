using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.RewardPunishmentPriority.Handlers.Commands
{
    public class CreateRewardPunishmentPriorityCommandHandler : IRequestHandler<CreateRewardPunishmentPriorityCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.RewardPunishmentPriority> _RewardPunishmentPriorityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRewardPunishmentPriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.RewardPunishmentPriority> RewardPunishmentPriorityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RewardPunishmentPriorityRepository = RewardPunishmentPriorityRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateRewardPunishmentPriorityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            if (request.RewardPunishmentPriorityDto.Name == null)
            {
                response.Success = false;
                response.Message = "Creation Failed! RewardPunishmentPriority Name is Requires";
            }
            else
            {
                if (RewardPunishmentPriorityNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.RewardPunishmentPriorityDto.Name}' already exists.";
                    
                }
                else
                {
                    var RewardPunishmentPriority = _mapper.Map<Hrm.Domain.RewardPunishmentPriority>(request.RewardPunishmentPriorityDto);

                    RewardPunishmentPriority = await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentPriority>().Add(RewardPunishmentPriority);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = RewardPunishmentPriority.Id;
                }
            }

            return response;
        }
        private bool RewardPunishmentPriorityNameExists(CreateRewardPunishmentPriorityCommand request)
        {

            IQueryable<Domain.RewardPunishmentPriority> RewardPunishmentPrioritys = _RewardPunishmentPriorityRepository.Where(x => x.Name == request.RewardPunishmentPriorityDto.Name);

             return RewardPunishmentPrioritys.Any();
        }
    }
}
