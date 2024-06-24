using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HolidayType.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HolidayType.Handlers.Commands
{
    public class UpdateHolidayTypeCommandHandler : IRequestHandler<UpdateHolidayTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.HolidayType> _HolidayTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UpdateHolidayTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.HolidayType> HolidayTypeRepository)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _HolidayTypeRepository = HolidayTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateHolidayTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateHolidayTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.HolidayTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            //var HolidayTypeName = request.HolidayTypeDto.HolidayTypeName.ToLower();
            var HolidayTypeName = request.HolidayTypeDto.HolidayTypeName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.HolidayType> HolidayTypes = _HolidayTypeRepository.Where(x => x.HolidayTypeName.ToLower() == HolidayTypeName && x.HolidayTypeId != request.HolidayTypeDto.HolidayTypeId);

            IQueryable<Hrm.Domain.HolidayType> isSame = _HolidayTypeRepository.Where(x => x.HolidayTypeName.Trim().ToLower().Replace(" ", string.Empty) == HolidayTypeName && x.IsActive == request.HolidayTypeDto.IsActive && x.HolidayTypeId == request.HolidayTypeDto.HolidayTypeId);

            if (HolidayTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.HolidayTypeDto.HolidayTypeName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else if (isSame.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed Nothing Changed.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }

            else
            {

                var HolidayType = await _unitOfWork.Repository<Hrm.Domain.HolidayType>().Get(request.HolidayTypeDto.HolidayTypeId);

                if (HolidayType is null)
                {
                    throw new NotFoundException(nameof(HolidayType), request.HolidayTypeDto.HolidayTypeId);
                }

                _mapper.Map(request.HolidayTypeDto, HolidayType);

                await _unitOfWork.Repository<Hrm.Domain.HolidayType>().Update(HolidayType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = HolidayType.HolidayTypeId;

            }

            return response;
        }
    }
}
