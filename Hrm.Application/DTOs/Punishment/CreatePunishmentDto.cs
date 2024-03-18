using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Punishment
{
    public  class CreatePunishmentDto:IPunishmentDto
    {
        public int PunishmentId { get; set; }
        public string? PunishmentName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
