﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormSchema
{
    public class CreateFormSchemaDto: IFormSchemaDto
    {
        public int SchemaId { get; set; }
        public int FormId { get; set; }
        public int FieldId { get; set; }
        public int SectionId { get; set; }
        public int? Section { get; set; }
        public int? OrderNo { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
