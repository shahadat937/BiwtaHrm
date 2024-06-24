using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Language
{
    public class LanguageDto: ILanguageDto
    {
        public int LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
