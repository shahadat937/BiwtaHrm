using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Notification
{
    public class CreateNotificationReadByDto
    {
        public int Id { get; set; }
        public int? NotificationId { get; set; }
        public int? EmpId { get; set; }
    }
}
