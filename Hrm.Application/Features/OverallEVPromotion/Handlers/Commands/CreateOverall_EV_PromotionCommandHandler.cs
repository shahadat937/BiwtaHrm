using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Overall_EV_Promotion.Validators;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Overall_EV_Promotion.Handlers.Commands
{
    public class CreateOverall_EV_PromotionCommandHandler : IRequestHandler<CreateOverall_EV_Promotion, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.OverallEVPromotion> _Overall_EV_PromotionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOverall_EV_PromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.OverallEVPromotion> Overall_EV_PromotionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Overall_EV_PromotionRepository = Overall_EV_PromotionRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateOverall_EV_Promotion request, CancellationToken cancellationToken)
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
             //   var Overall_EV_PromotionName = request.Overall_EV_PromotionDto.Overall_EV_PromotionName.Trim().ToLower().Replace(" ", string.Empty);

              //  IQueryable<Hrm.Domain.Overall_EV_Promotion> Overall_EV_Promotions = _Overall_EV_PromotionRepository.Where(x => x.Overall_EV_PromotionName.ToLower().Replace(" ", string.Empty) == Overall_EV_PromotionName);


                if (Overall_EV_PromotionNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.Overall_EV_PromotionDto.OverallEVPromotionName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
                }
                else
                {
                    var Overall_EV_Promotion = _mapper.Map<Hrm.Domain.OverallEVPromotion>(request.Overall_EV_PromotionDto);

                    Overall_EV_Promotion = await _unitOfWork.Repository<Hrm.Domain.OverallEVPromotion>().Add(Overall_EV_Promotion);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Overall_EV_Promotion.OverallEVPromotionId;
                }
            }

            return response;
        }
        private bool Overall_EV_PromotionNameExists(CreateOverall_EV_Promotion request)
        {
            var Overall_EV_PromotionName = request.Overall_EV_PromotionDto.OverallEVPromotionName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable <Hrm.Domain.OverallEVPromotion> Overall_EV_Promotions = _Overall_EV_PromotionRepository.Where(x => x.OverallEVPromotionName.Trim().ToLower().Replace(" ", string.Empty) == Overall_EV_PromotionName);

             return Overall_EV_Promotions.Any();
        }
    }
}
