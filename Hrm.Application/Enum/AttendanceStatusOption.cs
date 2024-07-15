using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Enum
{
    public enum AttendanceStatusOption
    {
        // Need to set the number according to database entry
        Present = 1,
        Late = 2,
        Absent = 3,
        OnSiteVisit = 4,
        OnLeave = 5
    }
}
