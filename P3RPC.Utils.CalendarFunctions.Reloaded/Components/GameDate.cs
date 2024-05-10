using P3RPC.Utils.CalendarFunctions.Interfaces;
using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;
using SharedScans.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3RPC.Utils.CalendarFunctions.Reloaded.Components
{
    internal class GameDate
    {
        private string GetUGlobalWork_SIG = "48 89 5C 24 ?? 57 48 83 EC 20 48 8B 0D ?? ?? ?? ?? 33 DB";

        public WrapperContainer<IGameDate> Wrap_GameDate;

        public IGameDate iface_GameDate;

        public UGlobalWork GlobalWork { get; private set; }

        public P3Date p3Date;

        public GameDate(ISharedScans scans) 
        {
            scans.AddScan<IGameDate.LoadGameInstance>(GetUGlobalWork_SIG);
            Wrap_GameDate = scans.CreateWrapper<IGameDate>(Mod.NAME);
            iface_GameDate = Wrap_GameDate.Wrapper;

            GlobalWork = iface_GameDate.GameInstance;

            iface_GameDate.PropertyChanged += ValueChanged;
        }

        private void ValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            p3Date = new P3Date(GlobalWork);
        }

        private struct P3DateBool
        {
            public bool IsTrue {  get; set; }

            /// <summary>
            /// Checks the current in-game date and returns true if the game has progressed past a specified date.
            /// </summary>
            /// <param name="dateStart">The date to be checked against.</param>
            /// <param name="globalWork">The current game instance.</param>
            public P3DateBool(P3Date dateStart, UGlobalWork globalWork)
            {
                var dateNow = new P3Date(globalWork);
                var dayStart = dateStart.DaysElapsed;
                var dayNow = dateNow.DaysElapsed;
                if (dayStart >= dayNow)
                {
                    IsTrue = true;
                }
                else
                {
                    IsTrue = false;
                }
            }
            /// <summary>
            /// Checks the current in-game date and returns true if the game's date is within a specified range.
            /// </summary>
            /// <param name="dateStart">The minimum date.</param>
            /// <param name="dateEnd">The maximum date.</param>
            /// <param name="globalWork">The current game instance.</param>
            public P3DateBool(P3Date dateStart, P3Date dateEnd, UGlobalWork globalWork)
            {
                var dateNow = new P3Date(globalWork);
                var dayStart = dateStart.DaysElapsed;
                var dayEnd = dateEnd.DaysElapsed;
                var dayNow = dateNow.DaysElapsed;
                if (dayNow >= dayStart && dayEnd >= dayNow)
                {
                    IsTrue = true;
                }
                else
                {
                    IsTrue = false;
                }
            }
            /// <summary>
            /// Checks the current in-game date and returns true if the game has progressed past a specified date.
            /// </summary>
            /// <param name="dateStart">The date to be checked against.</param>
            /// <param name="p3calendar">The current game calendar instance.</param>
            public P3DateBool(P3Date dateStart, Calendar p3calendar)
            {
                var dateNow = new P3Date(p3calendar);
                var dayStart = dateStart.DaysElapsed;
                var dayNow = dateNow.DaysElapsed;
                if (dayStart >= dayNow)
                {
                    IsTrue = true;
                }
                else
                {
                    IsTrue = false;
                }
            }
        }
    }
}
