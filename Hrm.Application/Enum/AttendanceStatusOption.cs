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
        Late = 3,
        Absent = 4,
        OnSiteVisit
    }
}
