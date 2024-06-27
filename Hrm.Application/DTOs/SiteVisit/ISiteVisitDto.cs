using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SiteVisit
{
    internal interface ISiteVisitDto
    {
        public int SiteVisitId { get; set; }
        public int EmpId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? VisitPlace { get; set; }
    }
}
