using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpForeignTourInfo
{
    public class EmpForeignTourInfoDto : IEmpForeignTourInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? CountryId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? Purpose { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
