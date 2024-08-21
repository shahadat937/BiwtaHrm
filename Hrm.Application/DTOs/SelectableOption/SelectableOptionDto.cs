using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SelectableOption
{
    public class SelectableOptionDto: ISelectionOptionDto
    {
        public int OptionId { get; set; }
        public int FieldId { get; set; }
        public string? FieldName { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
