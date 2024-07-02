using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttendanceStatus.Validators;
using Hrm.Application.DTOs.AttendanceType.Validators;
using Hrm.Application.Features.AttendanceStatus.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AttendanceStatus.Handlers.Commands
{
    public class CreateAttendanceStatusCommandHandler:IRequestHandler<CreateAttendanceStatusCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAttendanceStatusCommandHandler (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateAttendanceStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateAttendanceStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AttendanceStatusDto);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var AttendanceStatus = _mapper.Map<Hrm.Domain.AttendanceStatus>(request.AttendanceStatusDto);

            AttendanceStatus = await _unitOfWork.Repository<Hrm.Domain.AttendanceStatus>().Add(AttendanceStatus);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = AttendanceStatus.AttendanceStatusId;

            return response;
        }
    }
}
