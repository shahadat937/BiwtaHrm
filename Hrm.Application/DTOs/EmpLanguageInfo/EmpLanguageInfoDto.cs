using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpLanguageInfo
{
    public class EmpLanguageInfoDto : IEmpLanguageInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? LanguageId { get; set; }
        public int? CompetenceId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
