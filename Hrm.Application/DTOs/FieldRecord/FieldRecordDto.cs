using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FieldRecord
{
    public class FieldRecordDto: IFieldRecordDto
    {
        public int FieldRecordId { get; set; }
        public int FormRecordId { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
