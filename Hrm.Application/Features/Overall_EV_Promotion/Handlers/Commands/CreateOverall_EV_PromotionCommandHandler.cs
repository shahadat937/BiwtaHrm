using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Overall_EV_Promotion.Validators;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Overall_EV_Promotion.Handlers.Commands
{
    public class CreateOverall_EV_PromotionCommandHandler : IRequestHandler<CreateOverall_EV_PromotionCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOverall_EV_PromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateOverall_EV_PromotionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOverall_EV_PromotionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Overall_EV_PromotionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Overall_EV_Promotion = _mapper.Map<Hrm.Domain.Overall_EV_Promotion>(request.Overall_EV_PromotionDto);

                Overall_EV_Promotion = await _unitOfWork.Repository<Hrm.Domain.Overall_EV_Promotion>().Add(Overall_EV_Promotion);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Overall_EV_Promotion.Overall_EV_PromotionId;
            }

            return response;
        }

    }
}
