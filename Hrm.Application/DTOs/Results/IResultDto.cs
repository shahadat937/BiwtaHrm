using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Result
{
    public interface IResultDto
    {
        public int ResultId { get; set; }
        public string? ResultName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
