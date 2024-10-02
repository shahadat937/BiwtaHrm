using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Dashboard.Widgets
{
    public class WidgetsDto
    {
        public string? WidgetName { get; set; }
        public string? Label { get; set; }
        public List<string>? Labels { get; set; }
        public List<int>? Data { get; set; }
        public int? TotalPercentage { get; set; }
    }
}
