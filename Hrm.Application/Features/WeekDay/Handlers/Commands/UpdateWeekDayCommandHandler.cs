using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.WeekDay.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Weekend.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Weekend.Handlers.Commands
{
    public class UpdateWeekDayCommandHandler : IRequestHandler<UpdateWeekDayCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWeekDayCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateWeekDayCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateWeekDayDtoValidator();
            var validationResult = await validator.ValidateAsync(request.WeekendDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Weekend = await _unitOfWork.Repository<Hrm.Domain.WeekDay>().Get(request.WeekendDto.WeekDayId);

            if (Weekend is null)
            {
                throw new NotFoundException(nameof(Weekend), request.WeekendDto.WeekDayId);
            }

            _mapper.Map(request.WeekendDto, Weekend);

            await _unitOfWork.Repository<Hrm.Domain.WeekDay>().Update(Weekend);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
