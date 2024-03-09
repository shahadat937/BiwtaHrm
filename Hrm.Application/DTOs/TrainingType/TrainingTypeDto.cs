using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingType
{
    public class TrainingTypeDto : ITrainingTypeDto
    {
        public int TrainingTypeId { get; set; }
        public string TrainingTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
