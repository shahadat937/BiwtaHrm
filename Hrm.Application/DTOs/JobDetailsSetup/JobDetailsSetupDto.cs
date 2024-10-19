using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.JobDetailsSetup
{
    public class JobDetailsSetupDto : IJobDetailsSetupDto
    {
        public int Id { get; set; }
        public int? PRLAge { get; set; }
        public int? RetirmentAge { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
    }
}
