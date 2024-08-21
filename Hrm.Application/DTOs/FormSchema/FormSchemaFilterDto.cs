using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormSchema
{
    public class FormSchemaFilterDto
    {
        public int? SchemaId { get; set; }
        public int? FormId { get; set; }
        public int? FieldId { get; set; }
        public int? Section { get; set; }
        public bool? IsActive { get; set; }
    }
}
