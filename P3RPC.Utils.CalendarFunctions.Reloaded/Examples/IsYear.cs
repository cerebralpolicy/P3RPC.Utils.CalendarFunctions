using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;
using P3RPC.Utils.CalendarFunctions.Reloaded.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3RPC.Utils.CalendarFunctions.Reloaded.Examples
{
    public class IsYear
    {

        static P3Date NewYearsDay = new (2010, 01, 01);
        static P3Date NewYearsEve = new (2009, 12, 31);

        static void Yearis2009(GameDate gameDate)
        {
            var DateCheck = gameDate.IsDateAtMost(NewYearsEve);
            if (DateCheck)
            {
                Log.Information("It's still 2009.");
            }
        }
        static unsafe void Yearis2010(GameDate gameDate)
        {
            var DateCheck = gameDate.IsDateAtLeast(NewYearsDay);
            if (DateCheck)
            {
                Log.Information("It's now 2010!");
            }
        }

    }
}
