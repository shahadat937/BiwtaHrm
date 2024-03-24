using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Relation
{
    public  class CreateRelationDto:IRelationDto
    {
        public int RelationId { get; set; }
        public string? RelationName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
