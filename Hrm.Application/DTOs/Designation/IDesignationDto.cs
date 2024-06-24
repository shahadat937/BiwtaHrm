using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Designation
{
    public interface IDesignationDto
    {

        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public string? DesignationNameBangla { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
