using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTrainingInfo
{
    public class EmpTrainingInfoDto : IEmpTrainingInfoDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? TrainingTypeId { get; set; }
        public int? TrainingNameId { get; set; }
        public int? InstituteId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public int? TrainingDurationId { get; set; }
        public string? FileUrl { get; set; }
        public int? CountryId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public string? TrainingTypeName { get; set; }
        public string? TrainingName { get; set; }
        public string? InstituteName { get; set; }
        public string? TrainingDuration { get; set; }
        public string? CountryName { get; set; }
    }
}
