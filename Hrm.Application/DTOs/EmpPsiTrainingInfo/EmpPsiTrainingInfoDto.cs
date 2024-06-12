using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpPsiTrainingInfo
{
    public class EmpPsiTrainingInfoDto : IEmpPsiTrainingInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? TrainingNameId { get; set; }
        public string? WorkPurpose { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
