using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HolidayType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HolidayType.Handlers.Commands
{
    public class UpdateHolidayTypeCommandHandler : IRequestHandler<UpdateHolidayTypeCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHolidayTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateHolidayTypeCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateHolidayTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.HolidayTypeDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var HolidayType = await _unitOfWork.Repository<Hrm.Domain.HolidayType>().Get(request.HolidayTypeDto.HolidayTypeId);

            if (HolidayType is null)
            {
                throw new NotFoundException(nameof(HolidayType), request.HolidayTypeDto.HolidayTypeId);
            }

            _mapper.Map(request.HolidayTypeDto, HolidayType);

            await _unitOfWork.Repository<Hrm.Domain.HolidayType>().Update(HolidayType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
