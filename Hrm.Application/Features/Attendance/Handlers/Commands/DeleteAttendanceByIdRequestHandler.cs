using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Commands
{
    public class DeleteAttendanceByIdRequestHandler: IRequestHandler<DeleteAttendanceByIdRequest,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteAttendanceByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteAttendanceByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            if(request == null)
            {
                throw new BadRequestException("Provide AttendanceID To Delete");
            }

            var attendance = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Get(request.AttendanceId);

            if(attendance==null)
            {
                throw new NotFoundException(nameof(attendance),request.AttendanceId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Attendance>().Delete(attendance);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.AttendanceId;

            return response;
        }
    }
}
