using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Common
{
   public class QueryParams
    {
        public string? SearchText { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
