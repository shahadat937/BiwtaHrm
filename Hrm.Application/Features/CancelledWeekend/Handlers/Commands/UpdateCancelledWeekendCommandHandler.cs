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
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.CancelledWeekend.Handlers.Commands
{
    public class UpdateCancelledWeekendCommandHandler: IRequestHandler<UpdateCancelledWeekendCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateCancelledWeekendCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateCancelledWeekendCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateCancelledWeekendDtoValidator();
            var validationResult = await validator.ValidateAsync(request.cancelledWeekend);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var cancelledWeekend = await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Get(request.cancelledWeekend.Id);

            if(cancelledWeekend == null)
            {
                throw new NotFoundException(nameof(cancelledWeekend), request.cancelledWeekend.Id);
            }

            var weekends = await _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.year.YearName == request.cancelledWeekend.CancelDate.Year)
                .Include(x => x.year)
                .Include(x => x.weekDay)
                .ToListAsync();

            if (weekends.Any(w => w.weekDay.WeekDayName == request.cancelledWeekend.CancelDate.DayOfWeek.ToString())==false)
            {
                response.Success = false;
                response.Message = "Given date is not weekend";
                return response;
            }


            _mapper.Map(request.cancelledWeekend, cancelledWeekend);
            await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Update(cancelledWeekend);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = request.cancelledWeekend.Id;

            return response;

        }
    }
}
