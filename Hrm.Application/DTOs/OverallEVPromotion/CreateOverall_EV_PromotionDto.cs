using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Overall_EV_Promotion
{
    public  class CreateOverall_EV_PromotionDto:IOverall_EV_PromotionDto
    {
        public int OverallEVPromotionId { get; set; }
        public string? OverallEVPromotionName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
