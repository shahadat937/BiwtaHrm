using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Constants
{
    public class LeaveRule
    {
        public static readonly string MaxDaysPerRequest = "MaxDaysPerRequest";
        public static readonly string MaxDaysPerYear = "MaxDaysPerYear";
        public static readonly string MaxDaysPerMonth = "MaxDaysPerMonth";
        public static readonly string MaxDaysLifetime = "MaxDaysLifetime";
        public static readonly string AccrualRate = "AccrualRate";
        public static readonly string AccrualFrequency = "AccrualFrequency";
        public static readonly string MaxRequestLifeTime = "MaxRequestLifeTime";
        public static readonly string MinimumAge = "MinimumAge";
        public static readonly string Gender = "Gender";
        public static readonly string ApplyFreq = "ApplyFreq";
    }
}
