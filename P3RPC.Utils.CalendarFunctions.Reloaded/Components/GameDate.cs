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

namespace P3RPC.Utils.CalendarFunctions.Reloaded.Components
{
    internal class GameDate
    {
        private string GetUGlobalWork_SIG = "48 89 5C 24 ?? 57 48 83 EC 20 48 8B 0D ?? ?? ?? ?? 33 DB";

        DateFormatType DateFormat = new DateFormatType();

        public IGameDate? iface_GameDate;

        public IGameDate.loadUGlobalWork GetUGlobalWork { get; set; }
        public Locale dateLocale { get; private set; }

        public unsafe GameDate(ISharedScans scans, IStartupScanner startup, Config config) 
        {
            startup.AddMainModuleScan(GetUGlobalWork_SIG, MainModuleScanned);
            scans.AddScan<IGameDate.loadUGlobalWork>(GetUGlobalWork_SIG);
            var wrapperContainer = scans.CreateWrapper<IGameDate.loadUGlobalWork>(Mod.NAME);
            DateFormat = config.DateFormat;
            dateLocale =  new(DateFormat);
            GetUGlobalWork = wrapperContainer.Wrapper;
        }

        private void MainModuleScanned(PatternScanResult result)
        {
            Log.Debug($"[{Mod.NAME}] Module Signature is {result}.");
        }

        public unsafe string P3WeekDay(bool invariant)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var currently = new P3Date(globalWork);
            if (invariant) 
            {
                return currently.Date.ToString("dddd", CultureInfo.CreateSpecificCulture("en-us"));
            }
            else
            { 
                return currently.Date.ToString("dddd", CultureInfo.CreateSpecificCulture(dateLocale.CalendarLocale)); 
            }
        }


        public unsafe uint DaysOffset(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var currently = new P3Date(globalWork);
            if (currently.DaysElapsed > date.DaysElapsed)
            {
                return currently.DaysElapsed - date.DaysElapsed;
            }
            else
            {
                return date.DaysElapsed - currently.DaysElapsed;
            }
        }
        public unsafe uint WeeksOffset(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            var currently = new P3Date(globalWork);
            if (currently.DaysElapsed > date.DaysElapsed)
            {
                var days = (int)currently.DaysElapsed - (int)date.DaysElapsed;
                double weekD = days / 7;
                var weeks = Math.Floor(weekD);
                return (uint)weeks;
            }
            else
            {
                var days = date.DaysElapsed - currently.DaysElapsed;
                double weekD = days / 7;
                var weeks = Math.Floor(weekD);
                return (uint)weeks;
            }
        }
        public unsafe bool IsDateExactly(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            P3DateBool dateBool = new(date, globalWork, DateOperation.Exactly);
            bool MessageSent = false;
            var currently = new P3Date(globalWork);
            P3DateStrings formatted = new(date, currently, dateLocale);
            var condMSG = "DateAtLeast: TRUE";
            var debugMSG = $"The game is currently on {formatted.currently}";
            if (date.DaysElapsed != currently.DaysElapsed)
            {
                debugMSG = string.Concat(debugMSG, ", which falls");
            }
            if (currently.DaysElapsed < date.DaysElapsed)
            {
                var offset = date.DaysElapsed - currently.DaysElapsed;
                var s = "";
                if (offset > 1)
                {
                    s = "s";
                }
                condMSG = "DateAtLeast: FALSE";
                debugMSG = string.Join(" ", $"{offset} day{s} before {formatted.start}.");
                //Log.Verbose(debugMSG);
            }
            else if (currently.DaysElapsed > date.DaysElapsed)
            {
                var offset = currently.DaysElapsed - date.DaysElapsed;
                var s = "";
                if (offset > 1)
                {
                    s = "s";
                }
                condMSG = "DateAtLeast: FALSE";
                debugMSG = string.Join(" ", $"{offset} day{s} after {formatted.start}.");
                //Log.Verbose(debugMSG);
            }
            else
            {
                debugMSG = string.Concat(debugMSG, $".");
            }
            if (!MessageSent)
            {
                Log.Debug(condMSG);
                Log.Verbose(debugMSG);
                MessageSent = true;
            }
            return dateBool.IsTrue;
        }
        public unsafe bool IsDateAtLeast(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            P3DateBool dateBool = new(date, globalWork);
            bool MessageSent = false;
            var currently = new P3Date(globalWork);
            P3DateStrings formatted = new(date, currently, dateLocale);
            var condMSG = "DateAtLeast: TRUE";
            var debugMSG = $"The game is currently on {formatted.currently}";
            if (date.DaysElapsed != currently.DaysElapsed)
            {
                debugMSG = string.Concat(debugMSG, ", which falls");
            }
            if (currently.DaysElapsed < date.DaysElapsed)
            {
                var offset = date.DaysElapsed - currently.DaysElapsed;
                var s = "";
                if (offset > 1)
                {
                    s = "s";
                }
                condMSG = "DateAtLeast: FALSE";
                debugMSG = string.Join(" ", $"{offset} day{s} before {formatted.start}.");
                //Log.Verbose(debugMSG);
            }
            else if (currently.DaysElapsed > date.DaysElapsed)
            {
                var offset = currently.DaysElapsed - date.DaysElapsed;
                var s = "";
                if (offset > 1)
                {
                    s = "s";
                }
                debugMSG = string.Join(" ", $"{offset} day{s} after {formatted.start}.");
                //Log.Verbose(debugMSG);
            }
            else
            {
                debugMSG = string.Concat(debugMSG, $".");
            }
            if (!MessageSent)
            {           
                Log.Debug(condMSG);
                Log.Verbose(debugMSG);
                MessageSent = true;
            }
            return dateBool.IsTrue;
        }

        public unsafe bool IsDateAtMost(P3Date date)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            P3DateBool dateBool = new(date, globalWork, DateOperation.AtMost);
            bool MessageSent = false;
            var currently = new P3Date(globalWork);
            P3DateStrings formatted = new(date, currently, dateLocale);
            var condMSG = "DateAtMost: TRUE";
            var debugMSG = $"The game is currently on {formatted.currently}";
            if (date.DaysElapsed != currently.DaysElapsed)
            {
                debugMSG = string.Concat(debugMSG, ", which falls");
            }
            if (currently.DaysElapsed > date.DaysElapsed)
            {
                var offset = date.DaysElapsed - currently.DaysElapsed;
                var s = "";
                if (offset > 1)
                {
                    s = "s";
                }
                condMSG = "DateAtMost: FALSE";
                debugMSG = string.Join(" ", $"{offset} day{s} after {formatted.start}.");
                //Log.Verbose(debugMSG);
            }
            else if (currently.DaysElapsed > date.DaysElapsed)
            {
                var offset = currently.DaysElapsed - date.DaysElapsed;
                var s = "";
                if (offset > 1)
                {
                    s = "s";
                }
                debugMSG = string.Join(" ", $"{offset} day{s} before {formatted.start}.");
                //Log.Verbose(debugMSG);
            }
            else
            {
                debugMSG = string.Concat(debugMSG, $".");
            }
            if (!MessageSent)
            {
                Log.Debug(condMSG);
                Log.Verbose(debugMSG);
                MessageSent = true;
            }
            return dateBool.IsTrue;
        }

        public unsafe bool IsDateInRange(P3Date start, P3Date end)
        {
            UGlobalWork* globalWork = GetUGlobalWork.Invoke();
            P3DateBool dateBool = new(start,end,globalWork);
            bool MessageSent = false;
            var currently = new P3Date(globalWork);
            P3DateStrings formatted = new(start, end, currently, dateLocale);
            var debugMSG = $"The game is currently on {formatted.currently}, which falls";
            if (dateBool.IsTrue && !MessageSent)
            {
                debugMSG = string.Join(" ", $"within the defined range of {formatted.start} to {formatted.end}.");
                Log.Debug("DateInRange: TRUE");
                Log.Verbose(debugMSG);
            }
            else if (!dateBool.IsTrue && !MessageSent)
            {
                Log.Debug("DateInRange: FALSE");
                if (currently.DaysElapsed < start.DaysElapsed)
                {
                    var offset = start.DaysElapsed - currently.DaysElapsed;
                    var s = "";
                    if (offset > 1)
                    {
                        s = "s";
                    }
                    debugMSG = string.Join(" ", $"{offset} day{s} before {formatted.start}.");
                    Log.Verbose(debugMSG);
                }
                else
                {
                    var offset = currently.DaysElapsed - start.DaysElapsed;
                    var s = "";
                    if (offset > 1)
                    {
                        s = "s";
                    }
                    debugMSG = string.Join(" ", $"{offset} day{s} before {formatted.end}.");
                    Log.Verbose(debugMSG);
                }
            }
            return dateBool.IsTrue;
        }

        

        private struct P3DateStrings
        {
            public string start;
            public string? end;
            public string currently;

            public P3DateStrings(P3Date dateStart, P3Date dateNow, Locale locale)
            {
                start = locale.PrintDate(dateStart);
                currently = locale.PrintDate(dateNow);
            }

            public P3DateStrings(P3Date dateStart, P3Date dateEnd, P3Date dateNow, Locale locale)
            {
                start=locale.PrintDate(dateStart);
                end=locale.PrintDate(dateEnd);
                currently=locale.PrintDate(dateNow);
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
