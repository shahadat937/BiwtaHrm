using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.appraisalFormType
{
    public class appraisalFormTypeDto: IappraisalFormTypeDto
    {
        public int appraisalFormTypeId { get; set; }
        public string? appraisalFormTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
