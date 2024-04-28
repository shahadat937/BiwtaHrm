using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Overall_EV_Promotion.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Overall_EV_Promotion.Handlers.Commands
{
    public class UpdateOverall_EV_PromotionCommandHandler : IRequestHandler<UpdateOverall_EV_PromotionCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.OverallEVPromotion> _Overall_EV_PromotionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOverall_EV_PromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.OverallEVPromotion> Overall_EV_PromotionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Overall_EV_PromotionRepository = Overall_EV_PromotionRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOverall_EV_PromotionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateOverall_EV_PromotionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Overall_EV_PromotionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Overall_EV_Promotion = await _unitOfWork.Repository<Hrm.Domain.OverallEVPromotion>().Get(request.Overall_EV_PromotionDto.OverallEVPromotionId);

            if (Overall_EV_Promotion is null)
            {
                throw new NotFoundException(nameof(Overall_EV_Promotion), request.Overall_EV_PromotionDto.OverallEVPromotionId);
            }

            //var Overall_EV_PromotionName = request.Overall_EV_PromotionDto.Overall_EV_PromotionName.ToLower();
            var Overall_EV_PromotionName = request.Overall_EV_PromotionDto.OverallEVPromotionName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.OverallEVPromotion> Overall_EV_Promotions = _Overall_EV_PromotionRepository.Where(x => x.OverallEVPromotionName.ToLower() == Overall_EV_PromotionName);


            if (Overall_EV_Promotions.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.Overall_EV_PromotionDto.OverallEVPromotionName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.Overall_EV_PromotionDto, Overall_EV_Promotion);

                await _unitOfWork.Repository<Hrm.Domain.OverallEVPromotion>().Update(Overall_EV_Promotion);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Overall_EV_Promotion.OverallEVPromotionId;

            }
            return response;
        }
    }
}
