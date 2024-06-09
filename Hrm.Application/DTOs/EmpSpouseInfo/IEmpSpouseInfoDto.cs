using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpSpouseInfo
{
    public interface IEmpSpouseInfoDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
