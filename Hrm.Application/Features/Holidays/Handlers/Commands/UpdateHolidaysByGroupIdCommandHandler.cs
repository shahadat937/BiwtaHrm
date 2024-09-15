using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Holidays.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.DTOs.Holidays.Validators;

namespace Hrm.Application.Features.Holidays.Handlers.Commands
{
    public class UpdateHolidaysByGroupIdCommandHandler: IRequestHandler<UpdateHolidaysByGroupIdCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHolidaysByGroupIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateHolidaysByGroupIdCommand request, CancellationToken cancellationToken)
        {
            var holidays = await _unitOfWork.Repository<Hrm.Domain.Holidays>().Where(x => x.GroupId == request.GroupId).ToListAsync();

            if(holidays.Count()<=0)
            {
                throw new NotFoundException(nameof(holidays), request.GroupId);
            }

            foreach(var holiday in holidays)
            {
                await _unitOfWork.Repository<Hrm.Domain.Holidays>().Delete(holiday);
            }

            for(DateOnly curDate = request.From; curDate <= request.To;curDate = curDate.AddDays(1))
            {
                request.HolidayDto.HolidayDate = curDate;


                var validator = new UpdateHolidayDtoValidator();
                var validationResult = await validator.ValidateAsync(request.HolidayDto);
                if(!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult);
                }


                var holiday = _mapper.Map<Hrm.Domain.Holidays>(request.HolidayDto);

                // Set If weekend
                bool IsWeekend = await _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.weekDay.WeekDayName == holiday.HolidayDate.Value.DayOfWeek.ToString() && x.YearId == holiday.YearId).AnyAsync();

                bool IsCancelled = await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Where(x => DateOnly.FromDateTime(x.CancelDate) == curDate).AnyAsync();
                holiday.IsWeekend = IsWeekend & (!IsCancelled);
                holiday.GroupId = request.GroupId;

                holiday.HolidayId = 0;
                await _unitOfWork.Repository<Hrm.Domain.Holidays>().Add(holiday);
            }

            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Update Successful";
            response.Id = request.GroupId;

            return response;
        }
    }
}
