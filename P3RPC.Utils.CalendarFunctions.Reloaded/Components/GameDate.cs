using P3RPC.Utils.CalendarFunctions.Interfaces;
using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;
using P3RPC.Utils.CalendarFunctions.Reloaded.Configuration;
using Reloaded.Memory.Sigscan.Definitions.Structs;
using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using SharedScans.Interfaces;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace P3RPC.Utils.CalendarFunctions.Reloaded.Components
{
    internal unsafe class GameDate: INotifyPropertyChanged
    {
        private string GetUGlobalWork_SIG = "48 89 5C 24 ?? 57 48 83 EC 20 48 8B 0D ?? ?? ?? ?? 33 DB";

        public event PropertyChangedEventHandler? PropertyChanged;

        private void DayCountChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                Log.Debug($"Internal day count: {DayCount}");
            }
        }


        /// <summary>
        /// The Wrapper Container
        /// </summary>
        public WrapperContainer<IGameDate.loadUGlobalWork> ContainUGlobalWork { get; set; }
        public IGameDate.loadUGlobalWork GetUGlobalWork { get; set; }
        private uint CachedDayCount = 0;
        public uint DayCount {  
            get
            {
                return CachedDayCount;
            } 
            set
            {
                if (CachedDayCount != value)
                {
                    CachedDayCount = value;
                    DayCountChanged();
                }
            }
        }

        public unsafe GameDate(ISharedScans scans, IStartupScanner startup) 
        {
            startup.AddMainModuleScan(GetUGlobalWork_SIG, MainModuleScanned);
            scans.AddScan<IGameDate.loadUGlobalWork>(GetUGlobalWork_SIG);
            var wrapperContainer = scans.CreateWrapper<IGameDate.loadUGlobalWork>(Mod.NAME);
            GetUGlobalWork = wrapperContainer.Wrapper;
            ContainUGlobalWork = wrapperContainer;
            Log.Information("Loaded GameDate");
            Log.Debug($"Internal day count: {DayCount}");
        }

        public unsafe void UpdateDayCount() {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            if (globalWork != null)
            {
                DayCount = globalWork->Calendar.DaysSinceApril1;
            };
        }
        public unsafe void PrintDate()
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            if (globalWork != null)
            {
                var date = new P3Date(globalWork->Calendar).Date;
                Log.Information(date.ToShortDateString(),true);
            }
        }

        public unsafe bool IsDayAtLeast(uint DaysElapsed)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var GlobalElapsed = globalWork->Calendar.DaysSinceApril1;
            return DaysElapsed >= GlobalElapsed;
        }

        public unsafe bool IsDateAtLeast(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var GlobalElapsed = globalWork->Calendar.DaysSinceApril1;
            var DaysElapsed = date.DaysElapsed;
            return DaysElapsed >= GlobalElapsed;
        }

        private void MainModuleScanned(PatternScanResult result)
        {
            Log.Debug($"[{Mod.NAME}] Module Signature is {result}.");
        }


        private struct P3DateBool
        {
            public bool IsTrue {  get; set; }

            /// <summary>
            /// Checks the current in-game date and returns true if the game has progressed past a specified date.
            /// </summary>
            /// <param name="dateStart">The date to be checked against.</param>
            /// <param name="globalWork">The current game instance.</param>
            public unsafe P3DateBool(P3Date dateStart, UGlobalWork* globalWork, DateOperation operation = DateOperation.AtLeast)
            {
                var dateNow = new P3Date(globalWork);
                var dayStart = dateStart.DaysElapsed;
                var dayNow = dateNow.DaysElapsed;
                if (operation == DateOperation.AtMost)
                {
                    if (dayNow <= dayStart)
                    {
                        IsTrue = true;
                    }
                    else
                    {
                        IsTrue = false;
                    }
                }
                else if (operation == DateOperation.Exactly)
                {
                    if (dayNow == dayStart)
                    {
                        IsTrue = true;
                    }
                    else
                    {
                        IsTrue = false;
                    }
                }
                else
                {
                    if (dayNow >= dayStart)
                    {
                        IsTrue = true;
                    }
                    else
                    {
                        IsTrue = false;
                    }
                }
            }
            /// <summary>
            /// Checks the current in-game date and returns true if the game's date is within a specified range.
            /// </summary>
            /// <param name="dateStart">The minimum date.</param>
            /// <param name="dateEnd">The maximum date.</param>
            /// <param name="globalWork">The current game instance.</param>
            public unsafe P3DateBool(P3Date dateStart, P3Date dateEnd, UGlobalWork* globalWork)
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
