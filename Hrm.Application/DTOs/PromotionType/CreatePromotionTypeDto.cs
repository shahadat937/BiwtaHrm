using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PromotionType
{
    public class CreatePromotionTypeDto : IPromotionTypeDto
    {
        public int PromotionTypeId { get; set; }
        public string PromotionTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
