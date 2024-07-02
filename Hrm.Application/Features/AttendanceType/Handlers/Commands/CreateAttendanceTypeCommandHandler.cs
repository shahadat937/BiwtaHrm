using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttendanceType.Validators;
using Hrm.Application.DTOs.DayType.Validators;
using Hrm.Application.Features.AttendanceType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Hrm.Application.Features.AttendanceType.Handlers.Commands
{
    public class CreateAttendanceTypeCommandHandler:IRequestHandler<CreateAttendanceTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAttendanceTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateAttendanceTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateAttendanceTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AttendanceTypeDto);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var AttendanceType = _mapper.Map<Hrm.Domain.AttendanceType>(request.AttendanceTypeDto);

            AttendanceType = await _unitOfWork.Repository<Hrm.Domain.AttendanceType>().Add(AttendanceType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = AttendanceType.AttendanceTypeId;

            return response;
        }
    }
}
