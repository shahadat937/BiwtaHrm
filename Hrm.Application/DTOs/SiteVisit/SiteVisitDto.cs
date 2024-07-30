using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SiteVisit
{
    public class SiteVisitDto: ISiteVisitDto
    {
        public int SiteVisitId { get; set; }
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? VisitPlace { get; set; }
        public string? VisitPurpose { get; set; }
        public string? Remark { get; set; }
        public string? Status { get; set; }
    }
}
