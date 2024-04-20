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
        private readonly IHrmRepository<Hrm.Domain.PromotionType> _PromotionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePromotionTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.PromotionType> PromotionTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PromotionTypeRepository = PromotionTypeRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreatePromotionTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePromotionTypeDtoValidator();
            var validationPromotionType = await validator.ValidateAsync(request.PromotionTypeDto);

            if (validationPromotionType.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationPromotionType.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var PromotionTypeName = request.PromotionTypeDto.PromotionTypeName.ToLower();

                IQueryable<Hrm.Domain.PromotionType> PromotionTypes = _PromotionTypeRepository.Where(x => x.PromotionTypeName.ToLower() == PromotionTypeName);


                if (PromotionTypeNameExists(request))
                {
                    response.Success = false;
                   //response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.PromotionTypeDto.PromotionTypeName}' already exists.";

                    response.Errors = validationPromotionType.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var PromotionType = _mapper.Map<Hrm.Domain.PromotionType>(request.PromotionTypeDto);

                    PromotionType = await _unitOfWork.Repository<Hrm.Domain.PromotionType>().Add(PromotionType);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = PromotionType.PromotionTypeId;
                }
            }

            return response;
        }
        private bool PromotionTypeNameExists(CreatePromotionTypeCommand request)
        {
            var PromotionTypeName = request.PromotionTypeDto.PromotionTypeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.PromotionType> PromotionTypes = _PromotionTypeRepository.Where(x => x.PromotionTypeName.Trim().ToLower().Replace(" ", string.Empty) == PromotionTypeName);

            return PromotionTypes.Any();
        }
    }
}
