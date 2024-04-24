using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HairColor
{
    public class HairColorDto: IHairColorDto
    {
        public int HairColorId { get; set; }
        public string? HairColorName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
