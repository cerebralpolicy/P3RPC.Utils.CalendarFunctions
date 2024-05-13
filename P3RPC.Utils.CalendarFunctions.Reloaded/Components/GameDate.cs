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
    internal unsafe class GameDate: IGameDate
    {
        private string GetUGlobalWork_SIG = "48 89 5C 24 ?? 57 48 83 EC 20 48 8B 0D ?? ?? ?? ?? 33 DB";

        public IGameDate.loadUGlobalWork GetUGlobalWork { get; set; }

        public unsafe GameDate(ISharedScans scans, IStartupScanner startup) 
        {
            startup.AddMainModuleScan(GetUGlobalWork_SIG, MainModuleScanned);
            scans.AddScan<IGameDate.loadUGlobalWork>(GetUGlobalWork_SIG);
            var wrapperContainer = scans.CreateWrapper<IGameDate.loadUGlobalWork>(Mod.NAME);
            GetUGlobalWork = wrapperContainer.Wrapper;
            Log.Information("Loaded GameDate");
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

        #region Day-Based Bools

        public unsafe bool IsDayAtLeast(uint daysElapsed)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            P3DateOperationInfo(DateOperation.AtLeast, daysElapsed, globalElapsed);
            return daysElapsed >= globalElapsed;
        }

        public unsafe bool IsDayExactly(uint daysElapsed)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            P3DateOperationInfo(DateOperation.Exactly, daysElapsed, globalElapsed);
            return daysElapsed == globalElapsed;
        }

        public unsafe bool IsDayAtMost(uint daysElapsed)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            P3DateOperationInfo(DateOperation.AtMost, daysElapsed, globalElapsed);
            return daysElapsed <= globalElapsed;
        }

        public unsafe bool IsDayInRange(uint daysStart, uint daysEnd)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            P3DateOperationInfo(DateOperation.InRange, daysStart, globalElapsed, daysEnd);
            return (daysEnd <= globalElapsed) && (daysStart >= globalElapsed);
        }

        #endregion
        #region P3DateBasedBools

        public unsafe bool IsDateAtLeast(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            var daysElapsed = date.DaysElapsed;
            P3DateOperationInfo(DateOperation.AtLeast, daysElapsed, globalElapsed);
            return daysElapsed >= globalElapsed;
        }

        public unsafe bool IsDateExactly(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            var daysElapsed = date.DaysElapsed;
            P3DateOperationInfo(DateOperation.Exactly, daysElapsed, globalElapsed);
            return daysElapsed == globalElapsed;
        }

        public unsafe bool IsDateAtMost(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            var daysElapsed = date.DaysElapsed;
            P3DateOperationInfo(DateOperation.AtMost, daysElapsed, globalElapsed);
            return daysElapsed <= globalElapsed;
        }
        public unsafe bool IsDateInRange(P3Date dateStart, P3Date dateEnd)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var globalElapsed = globalWork->Calendar.DaysSinceApril1;
            var daysStart = dateStart.DaysElapsed;
            var daysEnd = dateEnd.DaysElapsed;
            P3DateOperationInfo(DateOperation.InRange,daysStart, globalElapsed, daysEnd);
            return (daysEnd <= globalElapsed) && (daysStart >= globalElapsed);
        }

        #endregion

        private void MainModuleScanned(PatternScanResult result)
        {
            Log.Debug($"Module Signature is {result}.");
        }

        private static void P3DateOperationInfo (DateOperation operation, uint minDays, uint checkDay, uint maxDays = 364)
        {
            if (operation == DateOperation.AtLeast)
            {
                var rawoffset = checkDay - minDays;
                var temporal = "after";
                if (rawoffset < 0)
                {
                    temporal = "before";
                }
                var dayS = "day";
                var offset = Math.Abs(rawoffset);
                if (offset > 1)
                {
                    dayS = "days";
                }
                var thisBool = (checkDay >= minDays);
                var MinDate = new P3Date(minDays).Date;
                var CheckDate = new P3Date(checkDay).Date;
                if (checkDay != minDays)
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {CheckDate}, {offset} {dayS} {temporal} {MinDate}.");
                }
                else if (checkDay == minDays)
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {MinDate}.");
                }
            }
            else if (operation == DateOperation.AtMost)
            {
                var rawoffset = checkDay - minDays;
                var temporal = "after";
                if (rawoffset < 0)
                {
                    temporal = "before";
                }
                var dayS = "day";
                var offset = Math.Abs(rawoffset);
                if (offset > 1)
                {
                    dayS = "days";
                }
                var thisBool = (checkDay <= minDays);
                var MinDate = new P3Date(minDays).Date;
                var CheckDate = new P3Date(checkDay).Date;
                if (checkDay != minDays)
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {CheckDate}, {offset} {dayS} {temporal} {MinDate}.");
                }
                else if (checkDay == minDays)
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {MinDate}.");
                }
            }
            else if (operation == DateOperation.Exactly)
            {
                var rawoffset = checkDay - minDays;
                var temporal = "after";
                if (rawoffset < 0)
                {
                    temporal = "before";
                }
                var dayS = "day";
                var offset = Math.Abs(rawoffset);
                if (offset > 1)
                {
                    dayS = "days";
                }
                var thisBool = (checkDay == minDays);
                var MinDate = new P3Date(minDays).Date;
                var CheckDate = new P3Date(checkDay).Date;
                if (!thisBool)
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {CheckDate}, {offset} {dayS} {temporal} {MinDate}.");
                }
                else if (thisBool)
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {MinDate}.");
                }
            }
            else
            {
                uint rawoffset = 0;
                var MinDate = new P3Date(minDays).Date;
                var CheckDate = new P3Date(checkDay).Date;
                var MaxDate = new P3Date(maxDays).Date;
                var RefDate = MinDate;
                var temporal = "after";
                if (checkDay < minDays)
                {
                    rawoffset = minDays - checkDay;
                }
                else if (checkDay >= maxDays)
                {
                    rawoffset = checkDay - maxDays;
                    temporal = "after";
                    RefDate = MaxDate;
                }
                else
                {
                    rawoffset = checkDay - minDays;
                    temporal = "into";
                }
                var dayS = "day";
                var offset = Math.Abs(rawoffset);
                if (offset > 1)
                {
                    dayS = "days";
                }
                var thisBool = ((checkDay <= maxDays)&&(checkDay >= minDays));
                if (thisBool && (offset > 0))
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {CheckDate}, {offset} {dayS} {temporal} the window of {MinDate} to {MaxDate}.");
                }
                else if (thisBool && (offset == 0))
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {RefDate}.");
                }
                else if (!thisBool)
                {
                    Log.Information($"Date operation returned {thisBool}.");
                    Log.Debug($"Result: {thisBool} - P3R is currently on {CheckDate}, {offset} {dayS} {temporal} {RefDate}.");
                }
            }
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
