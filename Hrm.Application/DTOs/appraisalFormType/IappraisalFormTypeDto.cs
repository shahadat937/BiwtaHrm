using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.appraisalFormType
{
    public interface IappraisalFormTypeDto
    {
        public int appraisalFormTypeId { get; set; }
        public string? appraisalFormTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
