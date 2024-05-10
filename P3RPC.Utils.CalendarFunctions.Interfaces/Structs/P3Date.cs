using P3RPC.Utils.CalendarFunctions.Interfaces.Extensions;
using static P3RPC.Utils.CalendarFunctions.Interfaces.Hooks.GameDateHook;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P3RPC.Utils.CalendarFunctions.Interfaces.Structs;

public struct P3Date
{
    public uint DaysElapsed;
    public DateOnly Date;
    public unsafe P3Date(UGlobalWork* globalWork)
    {
        var cal = globalWork->Calendar;
        var DaysSinceApril = cal.DaysSinceApril1;
        DaysElapsed = DaysSinceApril;
        Date = new DateOnly((int)Year(DaysSinceApril), (int)Month(DaysSinceApril), (int)Day(DaysSinceApril));
    }
    public P3Date(Calendar calendar)
    {
        var DaysSinceApril = calendar.DaysSinceApril1;
        DaysElapsed = DaysSinceApril;
        Date = new DateOnly((int)Year(DaysSinceApril), (int)Month(DaysSinceApril), (int)Day(DaysSinceApril));
    }
    public P3Date(uint DaysSinceApril)
    {
        DaysElapsed = DaysSinceApril;
        Date = new DateOnly((int)Year(DaysSinceApril), (int)Month(DaysSinceApril), (int)Day(DaysSinceApril));
    }
    public P3Date(int Year, int Month, int Day)
    {
        Date = new DateOnly(Year, Month, Day);
        var month = Date.Month - 1;
        var _month = (EDateMonth)month;
        var _monthOrdered = MonthEx.GetMonthOrdered(_month);
        var _monthStart = MonthEx.GetMonthStart(_monthOrdered);
        var add = (int)_monthStart;
        var days = Date.Day - 1;
        var elapsed = add + days;
        DaysElapsed = (uint)elapsed;
    }
    public P3Date(DateOnly date)
    {
        Date = date;
        var month = date.Month - 1;
        var _month = (EDateMonth)month;
        var _monthOrdered = MonthEx.GetMonthOrdered(_month);
        var _monthStart = MonthEx.GetMonthStart(_monthOrdered);
        var add = (int)_monthStart;
        var days = date.Day - 1;
        var elapsed = add + days;
        DaysElapsed = (uint)elapsed;
    }
}
