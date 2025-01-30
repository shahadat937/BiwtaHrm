using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SiteVisit
{
    public class SiteVisitFilterDto
    {
        public int? SiteVisitId { get; set; }
        public int? EmpId { get; set; }
        public List<string>? Status { get; set; }
        
    }
}
