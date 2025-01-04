using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Notifications.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Infrastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Notifications.Handlers.Commands
{
    public class UpdateNotificationStatusCommandHandler : IRequestHandler<UpdateNotificationStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public UpdateNotificationStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> notificationHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationHub = notificationHub;
        }
        public async Task<BaseCommandResponse> Handle(UpdateNotificationStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var notificationReadByDto = _mapper.Map<NotificationReadBy>(request.NotificationReadByDto);


            notificationReadByDto = await _unitOfWork.Repository<NotificationReadBy>().Add(notificationReadByDto);
            await _unitOfWork.Save();


            await _notificationHub.Clients.All.SendAsync("userNotification", response);


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = notificationReadByDto.Id;

            return response;
        }
    }
}