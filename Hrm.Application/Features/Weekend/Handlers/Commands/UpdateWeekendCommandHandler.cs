using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Weekend.Validators;
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
    public class UpdateWeekendCommandHandler : IRequestHandler<UpdateWeekendCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWeekendCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateWeekendCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateWeekendDtoValidator();
            var validationResult = await validator.ValidateAsync(request.WeekendDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Weekend = await _unitOfWork.Repository<Hrm.Domain.Weekend>().Get(request.WeekendDto.WeekendId);

            if (Weekend is null)
            {
                throw new NotFoundException(nameof(Weekend), request.WeekendDto.WeekendId);
            }

            _mapper.Map(request.WeekendDto, Weekend);

            await _unitOfWork.Repository<Hrm.Domain.Weekend>().Update(Weekend);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
