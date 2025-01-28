using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class EmpCountOnReportingDto
    {
        public int? TotalAssigned { get; set; }
        public int? TotalNull { get; set; }
        public List<CountReportingInfo> CountReportingInfo { get; set; }
    }
    public class CountReportingInfo
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Count { get; set; }
    }
}
