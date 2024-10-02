using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.CancelledWeekend.Validators;
using Hrm.Application.Features.CancelledWeekend.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.CancelledWeekend.Handlers.Commands
{
    public class CreateCancelledWeekendCommandHandler: IRequestHandler<CreateCancelledWeekendCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCancelledWeekendCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateCancelledWeekendCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCancelledWeekendDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CancelledWeekend);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            // check if weekend
            var weekends = await _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.year.YearName == request.CancelledWeekend.CancelDate.Year)
                .Include(x => x.year)
                .Include(x => x.weekDay)
                .ToListAsync();

            if(weekends.Any(w=> w.weekDay.WeekDayName == request.CancelledWeekend.CancelDate.DayOfWeek.ToString())==false)
            {
                response.Success = false;
                response.Message = "Given date is not weekend";
                return response;
            }

            var cancelledWeekend = _mapper.Map<Hrm.Domain.CancelledWeekend>(request.CancelledWeekend);

            await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Add(cancelledWeekend);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = cancelledWeekend.Id;

            return response;
        }
    }
}
