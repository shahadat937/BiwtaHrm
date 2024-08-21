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

            var Holidays = _mapper.Map<Hrm.Domain.Holidays>(request.HolidayDto);

            bool IsWorkday = await _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.weekDay.WeekDayName == Holidays.HolidayDate.Value.DayOfWeek.ToString() && x.YearId == Holidays.YearId).AnyAsync();

            Holidays.IsWeekend = !IsWorkday;
            Holidays = await _unitOfWork.Repository<Hrm.Domain.Holidays>().Add(Holidays);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = Holidays.HolidayId;

            return response;
        }
    }
}
