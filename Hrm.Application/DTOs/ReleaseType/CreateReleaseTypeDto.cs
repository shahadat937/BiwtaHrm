using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ReleaseType
{
    public class CreateReleaseTypeDto : IReleaseTypeDto
    {
        public int ReleaseTypeId { get; set; }
        public string? ReleaseTypeName { get; set; }
        public bool? IsDeptRelease { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
