using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Holidays.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Holidays.Validators;
using FluentValidation;

namespace Hrm.Application.Features.Holidays.Handlers.Commands
{
    public class UpdateHolidaysCommandHandler:IRequestHandler<UpdateHolidaysCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHolidaysCommandHandler (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateHolidaysCommand request, CancellationToken cancellationToken)
        {
            var holiday = await _unitOfWork.Repository<Hrm.Domain.Holidays>().Get(request.HolidayDto.HolidayId);
            var validator = new UpdateHolidayDtoValidator();
            var validationResult = await validator.ValidateAsync(request.HolidayDto,cancellationToken);

            if(validationResult.IsValid==false)
            {
                throw new Hrm.Application.Exceptions.ValidationException(validationResult);
            }

            if(holiday == null)
            {
                throw new NotFoundException(nameof(request.HolidayDto),request.HolidayDto.HolidayId);
            }

            _mapper.Map(request.HolidayDto, holiday);

            await _unitOfWork.Repository<Hrm.Domain.Holidays>().Update(holiday);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Update Successful";

            return response;
            

        }
    }
}
