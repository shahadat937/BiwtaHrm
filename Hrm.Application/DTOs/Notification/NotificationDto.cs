using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Notification
{
    public class NotificationDto : INotificationDto
    {
        public int Id { get; set; }
        public int? FromEmpId { get; set; }
        public int? ToEmpId { get; set; }
        public string? EmpIdCard { get; set; }
        public int? ToDeptId { get; set; }
        public int? FeatureId { get; set; }
        public int? UnreadCount { get; set; }
        public bool? IsNotice { get; set; }
        public bool? ForAllUsers { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? NevigateLink { get; set; }
        public int? ForEntryId { get; set; }
        public bool? ReadStatus { get; set; }
        public string? FromEmpName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
