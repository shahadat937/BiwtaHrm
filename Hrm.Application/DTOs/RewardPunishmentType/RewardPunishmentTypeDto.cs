using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.RewardPunishmentType
{
    public class RewardPunishmentTypeDto : IRewardPunishmentTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsPriority { get; set; }
        public bool? IsWithdraw { get; set; }
        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
    }
}
