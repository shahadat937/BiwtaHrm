using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class IncrementPromotionCountDto
    {
        public int? TotalPromotionCount { get; set; }
        public int? TotalIncrementCount { get; set; }
        public int? TotalUpdateDesgnation { get; set; }
        public int? TotalApprove { get; set; }
        public int? TotalReject { get; set; }
        public int? TotalPending { get; set; }
        public int? TotalIncrementPromotion {  get; set; }
        public int? Totals { get; set; }
    }
}
