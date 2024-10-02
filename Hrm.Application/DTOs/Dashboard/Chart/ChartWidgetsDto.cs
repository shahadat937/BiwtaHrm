using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Dashboard.Chart
{
    public class ChartWidgetsDto
    {
        public List<string>? Labels { get; set; }
        public List<ChartWidgetsDataSets>? Datasets { get; set; }
    }

    public class ChartWidgetsDataSets
    {
        public List<int>? Data { get; set; }
        public List<string>? BackgroundColor { get; set; }
    }

}
