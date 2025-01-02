using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
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
    public class GetNotificationForUserRequestHandler : IRequestHandler<GetNotificationForUserRequest, PagedResult<NotificationDto>>
    {

        private readonly IHrmRepository<Notification> _NotificationRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IHrmRepository<Feature> _FeatureRepository;
        private readonly IHrmRepository<Domain.AspNetUsers> _AspNetUsersRepository;
        private readonly IHrmRepository<Domain.AspNetUserRoles> _AspNetUserRolesRepository;
        private readonly IHrmRepository<RoleFeature> _RoleFeatureRepository;
        private readonly IHrmRepository<NotificationReadBy> _NotificationReadByRepository;
        private readonly IMapper _mapper;
        public GetNotificationForUserRequestHandler(IHrmRepository<Notification> NotificationRepository, IMapper mapper, IHrmRepository<EmpJobDetail> EmpJobDetailRepository, IHrmRepository<Feature> FeatureRepository, IHrmRepository<Domain.AspNetUsers> aspNetUsersRepository, IHrmRepository<AspNetUserRoles> aspNetUserRolesRepository, IHrmRepository<RoleFeature> roleFeatureRepository, IHrmRepository<NotificationReadBy> notificationReadByRepository)
        {
            _NotificationRepository = NotificationRepository;
            _mapper = mapper;
            _EmpJobDetailRepository = EmpJobDetailRepository;
            _FeatureRepository = FeatureRepository;
            _AspNetUsersRepository = aspNetUsersRepository;
            _AspNetUserRolesRepository = aspNetUserRolesRepository;
            _RoleFeatureRepository = roleFeatureRepository;
            _NotificationReadByRepository = notificationReadByRepository;
        }

        public async Task<PagedResult<NotificationDto>> Handle(GetNotificationForUserRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the employee's department ID
            var empDeptId = await _EmpJobDetailRepository
                .Where(x => x.EmpId == request.EmpId)
                .Select(x => x.DepartmentId)
                .FirstOrDefaultAsync();

            // Retrieve the user's role ID
            var userId = await _AspNetUsersRepository
                .Where(x => x.EmpId == request.EmpId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var roleId = await _AspNetUserRolesRepository
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .FirstOrDefaultAsync();

            // Retrieve the FeatureKeys that the user has permission to view
            var accessibleFeatureIds = await _RoleFeatureRepository
                .Where(rf => rf.RoleId == roleId && rf.ViewStatus == true)
                .Select(f => f.FeatureKey)
                .ToListAsync();

            var query = _NotificationRepository
                .Where(n => n.ToEmpId == request.EmpId || (n.ToDeptId == empDeptId && accessibleFeatureIds.Contains(n.FeatureId.Value)) || n.ForAllUsers == true);

            var readNotificationIds = await _NotificationReadByRepository
                .Where(nrb => nrb.EmpId == request.EmpId)
                .Select(nrb => nrb.NotificationId.Value)
                .ToListAsync();


            var totalRecords = await query.CountAsync(cancellationToken);

            var unreadCount = totalRecords - readNotificationIds.Count();


            var notifications = await query
                .OrderBy(n => readNotificationIds.Contains(n.Id))
                    .ThenByDescending(n => n.Id)
                .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToListAsync(cancellationToken);

            var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);


            foreach (var notificationDto in notificationDtos)
            {
                notificationDto.UnreadCount = unreadCount;
                notificationDto.ReadStatus = readNotificationIds.Contains(notificationDto.Id);
            }


            var result = new PagedResult<NotificationDto>(notificationDtos, totalRecords, request.QueryParams.PageIndex, request.QueryParams.PageSize);

            return result;
        }
    }
}
