using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reward.Validators;
using Hrm.Application.Features.Reward.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Reward.Handlers.Commands
{
    public class CreateRewardCommandHandler : IRequestHandler<CreateRewardCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRewardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateRewardCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateRewardDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RewardDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Reward = _mapper.Map<Hrm.Domain.Reward>(request.RewardDto);

                Reward = await _unitOfWork.Repository<Hrm.Domain.Reward>().Add(Reward);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Reward.RewardId;
            }

            return response;
        }

    }
}
