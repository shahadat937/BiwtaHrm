using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using Hrm.Infrastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class DeletePendingDeviceCommandHandler : IRequestHandler<DeletePendingDeviceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public DeletePendingDeviceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> notificationHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationHub = notificationHub;
        }

        public async Task<BaseCommandResponse> Handle(DeletePendingDeviceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var pendingDevice = await _unitOfWork.Repository<Hrm.Domain.PendingDevice>().Get(request.PendingDeviceId);

            if(pendingDevice == null)
            {
                throw new NotFoundException("Pending Device",request.PendingDeviceId);
            }

            await _unitOfWork.Repository<Hrm.Domain.PendingDevice>().Delete(pendingDevice);
            await _unitOfWork.Save();

            await _notificationHub.Clients.All.SendAsync("newDevice", "Device Deleted");

            response.Success = true;
            response.Message = "Successful";
            response.Id = request.PendingDeviceId;

            return response;

        }
    }
}
