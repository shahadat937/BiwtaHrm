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
using Microsoft.EntityFrameworkCore;
using Hrm.Application.Helpers;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler: IRequestHandler<DeleteLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LeaveAtdHelper leaveAtdHelper;

        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            leaveAtdHelper = new LeaveAtdHelper(_unitOfWork, _mapper);
        }

        public async Task<BaseCommandResponse> Handle(DeleteLeaveRequestCommand request,  CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Get(request.LeaveRequestId);

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.LeaveRequestId);
            }

            if(leaveRequest.IsOldLeave==true)
            {
                leaveAtdHelper.leaveRequestId = leaveRequest.LeaveRequestId;
                await leaveAtdHelper.deleteAttendance();
            }
            await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Delete(leaveRequest);

            await _unitOfWork.Save();

            await DeleteFile();


            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.LeaveRequestId;
            return response;
        }

        private async Task DeleteFile()
        {
            var files = await _unitOfWork.Repository<Hrm.Domain.LeaveFiles>().Where(x => x.LeaveRequestId == null).ToListAsync();

            foreach (var file in files)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "leaveFile", file.FilePath);

                try
                {
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        await _unitOfWork.Repository<Hrm.Domain.LeaveFiles>().Delete(file);
                        await _unitOfWork.Save();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("File Deletion Failed");
                }
            }
        }
    }
}
