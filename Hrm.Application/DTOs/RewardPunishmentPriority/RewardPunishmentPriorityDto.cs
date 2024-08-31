using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RewardPunishmentPriority
{
    public class RewardPunishmentPriorityDto : IRewardPunishmentPriorityDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
    }
}
