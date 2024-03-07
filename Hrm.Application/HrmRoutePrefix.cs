using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hrm.Shared.Constant.Constants;

namespace Hrm.Application
{
    public static class HrmRoutePrefix
    {
        private const string HRMRoutePrefixBase = ApiRoutePrefix.RoutePrefixBase + "hrm/";


        public const string BloodGroup = HRMRoutePrefixBase + "blood-group";
        public const string MaritalStatus = HRMRoutePrefixBase + "marital-status";

        public const string EmployeeType = HRMRoutePrefixBase + "employee-type";
        public const string Gender = HRMRoutePrefixBase + "gender";
    }
}
