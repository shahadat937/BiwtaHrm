using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Holidays.Validators;
using Hrm.Application.Features.Holidays.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Handlers.Commands
{
    public class CreateHolidaysCommandHandler: IRequestHandler<CreateHolidaysCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHolidaysCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateHolidaysCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var validator = new CreateHolidayDtoValidator();

            var validationResult = await validator.ValidateAsync(request.HolidayDto);

            if(!validationResult.IsValid)
            {
                throw new Hrm.Application.Exceptions.ValidationException(validationResult);
            }

            int groupId = 0;

            if(await _unitOfWork.Repository<Hrm.Domain.Holidays>().Where(x=>true).CountAsync()>0)
            {
                groupId = (int)await _unitOfWork.Repository<Hrm.Domain.Holidays>().Where(x => true).Select(x => x.GroupId).MaxAsync() + 1;
            }

            for (DateOnly curdate = request.DateFrom; curdate <= request.DateTo; curdate= curdate.AddDays(1))
            {
                request.HolidayDto.HolidayDate = curdate;
                var holiday = _mapper.Map<Hrm.Domain.Holidays>(request.HolidayDto);

                bool IsWeekend = await _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.weekDay.WeekDayName == holiday.HolidayDate.Value.DayOfWeek.ToString() && x.YearId == holiday.YearId).AnyAsync();

                bool IsCancelled = await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Where(x => DateOnly.FromDateTime(x.CancelDate) == curdate).AnyAsync();

                holiday.IsWeekend = IsWeekend&(!IsCancelled);

                holiday.GroupId = groupId;

                await _unitOfWork.Repository<Hrm.Domain.Holidays>().Add(holiday);
            }
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }
    }
}
