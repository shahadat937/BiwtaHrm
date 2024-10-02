using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpTrainingInfo : BaseDomainEntity
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? TrainingTypeId { get; set; }
        public int? TrainingNameId { get; set; }
        public int? InstituteId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? TrainingDurationId { get; set; }
        public string? FileUrl { get; set; }
        public int? CountryId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        //public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        //public virtual TrainingType? TrainingType { get; set; }
        //public virtual TrainingName? TrainingName { get; set; }
        //public virtual Institute? Institute { get; set; }
        //public virtual Country? Country { get; set; }
    }
}