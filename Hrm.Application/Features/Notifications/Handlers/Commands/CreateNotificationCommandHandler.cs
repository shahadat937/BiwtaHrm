using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Features.Notifications.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Infrastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Notifications.Handlers.Commands
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Feature> _FeatureRepository;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Feature> featureRepository, IHubContext<NotificationHub> notificationHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _FeatureRepository = featureRepository;
            _notificationHub = notificationHub;
        }
        public async Task<BaseCommandResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var featureId = await _FeatureRepository.Where(x => x.Path == request.NotificationDto.FeaturePath).Select(x => x.FeatureId).FirstOrDefaultAsync();

            var notificationDto = _mapper.Map<Notification>(request.NotificationDto);

            notificationDto.FeatureId = featureId;

            notificationDto = await _unitOfWork.Repository<Notification>().Add(notificationDto);
            await _unitOfWork.Save();


            await _notificationHub.Clients.All.SendAsync("userNotification", response);


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = notificationDto.Id;

            return response;
        }
    }
}
