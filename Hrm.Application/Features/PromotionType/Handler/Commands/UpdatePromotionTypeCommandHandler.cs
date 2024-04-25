using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PromotionType.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Features.PromotionType.Request.Commands;

namespace Hrm.Application.Features.PromotionType.Handlers.Commands
{
    public class UpdatePromotionTypeCommandHandler : IRequestHandler< UpdatePromotionTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.PromotionType> _PromotionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePromotionTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.PromotionType> PromotionTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PromotionTypeRepository = PromotionTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdatePromotionTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdatePromotionTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PromotionTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var PromotionTypeName = request.PromotionTypeDto.PromotionTypeName.ToLower();
            var PromotionTypeName = request.PromotionTypeDto.PromotionTypeName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.PromotionType> PromotionTypes = _PromotionTypeRepository.Where(x => x.PromotionTypeName.ToLower() == PromotionTypeName);



            if (PromotionTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.PromotionTypeDto.PromotionTypeName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var PromotionType = await _unitOfWork.Repository<Hrm.Domain.PromotionType>().Get(request.PromotionTypeDto.PromotionTypeId);

                if (PromotionType is null)
                {
                    throw new NotFoundException(nameof(PromotionType), request.PromotionTypeDto.PromotionTypeId);
                }

                _mapper.Map(request.PromotionTypeDto, PromotionType);

                await _unitOfWork.Repository<Hrm.Domain.PromotionType>().Update(PromotionType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = PromotionType.PromotionTypeId;

            }

            return response;
        }
    }
}
