using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.RewardPunishmentType.Handlers.Commands
{
    public class CreateRewardPunishmentTypeCommandHandler : IRequestHandler<CreateRewardPunishmentTypeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.RewardPunishmentType> _RewardPunishmentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRewardPunishmentTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.RewardPunishmentType> RewardPunishmentTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RewardPunishmentTypeRepository = RewardPunishmentTypeRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateRewardPunishmentTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            if (request.RewardPunishmentTypeDto.Name == null)
            {
                response.Success = false;
                response.Message = "Creation Failed! RewardPunishmentType Name is Requires";
            }
            else
            {
                if (RewardPunishmentTypeNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.RewardPunishmentTypeDto.Name}' already exists.";
                    
                }
                else
                {
                    var RewardPunishmentType = _mapper.Map<Hrm.Domain.RewardPunishmentType>(request.RewardPunishmentTypeDto);

                    RewardPunishmentType = await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentType>().Add(RewardPunishmentType);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = RewardPunishmentType.Id;
                }
            }

            return response;
        }
        private bool RewardPunishmentTypeNameExists(CreateRewardPunishmentTypeCommand request)
        {

            IQueryable<Domain.RewardPunishmentType> RewardPunishmentTypes = _RewardPunishmentTypeRepository.Where(x => x.Name == request.RewardPunishmentTypeDto.Name);

             return RewardPunishmentTypes.Any();
        }
    }
}
