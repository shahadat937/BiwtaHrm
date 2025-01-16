using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Notification;
using Hrm.Application.Features.Notifications.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Notifications.Handlers.Queries
{
    public class GetNoticeListRequestHandler : IRequestHandler<GetNoticeListRequest, PagedResult<NotificationDto>>
    {

        private readonly IHrmRepository<Notification> _NotificationRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<NotificationReadBy> _NotificationReadByRepository;

        public GetNoticeListRequestHandler(IHrmRepository<Notification> NotificationRepository, IMapper mapper, IHrmRepository<NotificationReadBy> notificationReadByRepository)
        {
            _NotificationRepository = NotificationRepository;
            _mapper = mapper;
            _NotificationReadByRepository = notificationReadByRepository;
        }
        public async Task<PagedResult<NotificationDto>> Handle(GetNoticeListRequest request, CancellationToken cancellationToken)
        {
            var query = _NotificationRepository
                .Where(x => x.IsNotice == true);

            var readNotificationIds = await _NotificationReadByRepository
                .Where(nrb => nrb.EmpId == request.EmpId)
                .Select(nrb => nrb.NotificationId.Value)
                .ToListAsync();

            var totalRecords = await query.CountAsync(cancellationToken);

            var notifications = await query
                .OrderBy(x => x.Id)
                .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToListAsync(cancellationToken);


            var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);

            foreach (var notificationDto in notificationDtos)
            {
                notificationDto.ReadStatus = readNotificationIds.Contains(notificationDto.Id);
            }

            var result = new PagedResult<NotificationDto>(notificationDtos, totalRecords, request.QueryParams.PageIndex, request.QueryParams.PageSize);

            return result;
        }
    }
}
