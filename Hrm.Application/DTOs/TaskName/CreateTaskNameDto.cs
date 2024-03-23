using Hrm.Application.DTOs.TaskName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TaskName
{
    public class CreateTaskNameDto:ITaskNameDto
    {
        public int TaskNameId { get; set; }
        public string? TaskNames { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

    }

}
