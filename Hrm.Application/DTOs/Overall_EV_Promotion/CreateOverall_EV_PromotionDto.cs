using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Overall_EV_Promotion
{
    public  class CreateOverall_EV_PromotionDto:IOverall_EV_PromotionDto
    {
        public int Overall_EV_PromotionId { get; set; }
        public string? Overall_EV_PromotionName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
