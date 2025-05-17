using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OrderType
{
    public interface IOrderTypeDto
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
        public string? TypeNameBangla { get; set; }
        public bool? IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
