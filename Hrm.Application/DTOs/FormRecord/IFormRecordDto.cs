using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormRecord
{
    public interface IFormRecordDto
    {
        public int FormId { get; set; }
        public int EmpId { get; set; }
        public bool IsActive { get; set; }
    }
}
