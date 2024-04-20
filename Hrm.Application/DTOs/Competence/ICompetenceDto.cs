using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Competence
{
    public interface ICompetenceDto
    {
        public int CompetenceId { get; set; }
        public string? CompetenceName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
