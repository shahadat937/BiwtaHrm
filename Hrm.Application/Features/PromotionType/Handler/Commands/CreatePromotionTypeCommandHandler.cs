using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PromotionType.Validators;
using Hrm.Application.Features.PromotionType.Request.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PromotionType.Handler.Commands
{
    public class CreatePromotionTypeCommandHandler : IRequestHandler<CreatePromotionTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.PromotionType> _promotionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePromotionTypeCommandHandler(IHrmRepository<Hrm.Domain.PromotionType> promotionTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _promotionTypeRepository = promotionTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatePromotionTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePromotionTypeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.PromotionTypeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var promotionTypeName = request.PromotionTypeDto.PromotionTypeName.ToLower();

                IQueryable<Hrm.Domain.PromotionType> promotionTypes = _promotionTypeRepository.Where(x => x.PromotionTypeName.ToLower() == promotionTypeName);

                if (promotionTypes.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                }
                else
                {
                    var PromotionType = _mapper.Map<Hrm.Domain.PromotionType>(request.PromotionTypeDto);

                    PromotionType = await _unitOfWork.Repository<Hrm.Domain.PromotionType>().Add(PromotionType);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = PromotionType.PromotionTypeId;
                }
            }
            return response;
        }
    }
}
