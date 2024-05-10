using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Reloaded.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3RPC.Utils.CalendarFunctions.Reloaded.Examples
{
    internal class Foobaw
    {
        static P3Date NFLSeasonStart = new P3Date(2009, 09, 10);
        static P3Date NFLSeasonEnd = new P3Date(2010, 01, 07);
        static P3Date NFLPlayoffStart = new P3Date(2010, 01, 09);
        static P3Date NFLPlayoffEnd = new P3Date(2010, 02, 07);

        public Foobaw()
        {

        }

        static void NFLSeasonProgression(GameDate gameDate)
        {
            if (gameDate.IsDateInRange(NFLSeasonStart, NFLSeasonEnd))
            {
                var SeasonWeek = gameDate.WeeksOffset(NFLSeasonStart) + 1;
                if (gameDate.P3WeekDay(true) == "Tuesday")
                {
                    Log.Information($"The NFL has started Week {SeasonWeek}.");
                }
                else
                {
                    Log.Information($"The NFL is currently on Week {SeasonWeek}.");
                }
            }
            else if (gameDate.IsDateInRange(NFLPlayoffStart, NFLPlayoffEnd))
            {
                Log.Information("The NFL Playoffs are being held.");
            }
        }
    }
}
