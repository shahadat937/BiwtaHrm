using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingName
{
    public interface ITrainingNameDto
    {
        public int TrainingNameId { get; set; }
        public string TrainingNames { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
