using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EyesColor
{
    public  class CreateEyesColorDto:IEyesColorDto
    {
        public int EyesColorId { get; set; }
        public string? EyesColorName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
