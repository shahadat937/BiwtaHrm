using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.JobDetailsSetup
{
    public class CreateJobDetailsSetupDto : IJobDetailsSetupDto
    {
        public int Id { get; set; }
        public int? PRLAge { get; set; }
        public int? RetirmentAge { get; set; }
        public DateOnly? OrderStartDate { get; set; }
        public DateOnly? OrderEndDate { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
    }
}
