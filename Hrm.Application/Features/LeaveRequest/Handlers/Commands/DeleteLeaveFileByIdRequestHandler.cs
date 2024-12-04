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

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class DeleteLeaveFileByIdRequestHandler: IRequestHandler<DeleteLeaveFileByIdCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveFileByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteLeaveFileByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveFile = await _unitOfWork.Repository<Hrm.Domain.LeaveFiles>().Get(request.LeaveFileId);

            if(leaveFile == null)
            {
                throw new NotFoundException(nameof(leaveFile), request.LeaveFileId);
            }

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "leaveFile", leaveFile.FilePath);

            try
            {
                if (File.Exists(fullPath))
                {
                    await _unitOfWork.Repository<Hrm.Domain.LeaveFiles>().Delete(leaveFile);
                    File.Delete(fullPath);
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("File Deletion Failed");
            }

            response.Success = true;
            response.Message = "Deleted";
            response.Id = request.LeaveFileId;


            return response;
        }
    }
}
