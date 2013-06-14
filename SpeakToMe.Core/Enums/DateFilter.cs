using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Core.Enums
{
    public enum DateFilter
    {
        None = 0,
        Today,
        Yesterday,
        Tomorrow,
        ThisWeek,
        ThisMonth,
        ThisYear,
        LastWeek,
        LastMonth,
        Lastyear,
        NextWeek,
        NextMonth,
        NextYear,
        Last7Days,
        Last30Days,
        Last60Days,
        Last90Days,
        Last120Days,
    }
}
