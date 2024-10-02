using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRequest.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.LeaveRequest.Validators;
using Hrm.Application.Enum;
using Hrm.Application.Helpers;
using Hrm.Domain;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler: IRequestHandler<CreateLeaveRequestCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            if(request.createLeaveRequestDto == null)
            {
                throw new BadRequestException("Leave Request Data is not provided");
            }

            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator();
            var validationResult = await validator.ValidateAsync(request.createLeaveRequestDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }


            // Validating the Leave Request If it's acceptable
            var leaveValidator = new LeaveRequestValidator(_unitOfWork);

            bool leaveValidationResult = await leaveValidator.Validate(request.createLeaveRequestDto.FromDate, request.createLeaveRequestDto.ToDate, request.createLeaveRequestDto.EmpId, request.createLeaveRequestDto.LeaveTypeId);

            if(!leaveValidationResult)
            {
                response.Success = false;
                response.Message = "Leave amount is not enough";

                return response;
            }





            request.createLeaveRequestDto.Status = (int) LeaveStatusOption.Pending;
            var leaveRequest = _mapper.Map<Hrm.Domain.LeaveRequest>(request.createLeaveRequestDto);

            if (request.AssociatedFiles != null)
            {
                string uniqueFileName = GenerateUniqueFileName(Path.GetExtension(request.AssociatedFiles.FileName));

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\leaveFile", uniqueFileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (var signStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.AssociatedFiles.CopyToAsync(signStream);
                }
                leaveRequest.AssociatedFile = uniqueFileName;

            }


            await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Add(leaveRequest);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveRequest.LeaveRequestId;

            return response;
        }

        private string GenerateUniqueFileName(string extension)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var uniqueFileName = $"{timestamp}_{Guid.NewGuid().ToString()}{extension}";
            return uniqueFileName;
        }
    }
}
