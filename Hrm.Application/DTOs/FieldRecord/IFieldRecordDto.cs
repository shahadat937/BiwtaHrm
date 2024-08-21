using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FieldRecord
{
    public interface IFieldRecordDto
    {
        public int FormRecordId { get; set; }
        public int FieldId { get; set; }
        public string FieldValue { get; set; }
        public bool IsActive { get; set; }
    }
}
