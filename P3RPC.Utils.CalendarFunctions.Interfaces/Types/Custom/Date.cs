using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace P3RPC.Utils.CalendarFunctions.Interfaces.Types;

public struct Year
{
    public Month April;
    public Month May;
    public Month June;
    public Month July;
    public Month August;
    public Month September;
    public Month October;
    public Month November;
    public Month December;
    public Month January;
    public Month February;
    public Month March;

    public Year()
    {
        April = new(EDateMonth.April,30);
        May = new(EDateMonth.May,31);
        June = new(EDateMonth.June,30); 
        July = new(EDateMonth.July,31);
        August = new(EDateMonth.August,31);
        September = new(EDateMonth.September,30);
        October = new(EDateMonth.October, 31);
        November = new(EDateMonth.November, 30);
        December = new(EDateMonth.December, 31);
        January = new(EDateMonth.January, 31);
        February = new(EDateMonth.February, 28);
        March = new(EDateMonth.March, 31);
    }
}

public static class MonthExtensions
{
    public static EDateMonthOrdered GetMonthOrdered(EDateMonth month)
        => month switch
        {
            EDateMonth.January => EDateMonthOrdered.January,
            EDateMonth.February => EDateMonthOrdered.February,
            EDateMonth.March => EDateMonthOrdered.March,
            EDateMonth.April => EDateMonthOrdered.April,
            EDateMonth.May => EDateMonthOrdered.May,
            EDateMonth.June => EDateMonthOrdered.June,
            EDateMonth.July => EDateMonthOrdered.July,
            EDateMonth.August => EDateMonthOrdered.August,
            EDateMonth.September => EDateMonthOrdered.September,
            EDateMonth.October => EDateMonthOrdered.October,
            EDateMonth.November => EDateMonthOrdered.November,
            EDateMonth.December => EDateMonthOrdered.December,
            EDateMonth.MonthMax => EDateMonthOrdered.MonthOrderedMax,
            EDateMonth.EDateMonth_Max => EDateMonthOrdered.EDateMonthOrdered_MAX,
            _ => throw new InvalidEnumArgumentException(nameof(month),(int)month,typeof(EDateMonth)),
        };
    public static EDateMonthStartOrdered GetMonthStart(EDateMonthOrdered month)
        => month switch
        {
            EDateMonthOrdered.April => EDateMonthStartOrdered.April,
            EDateMonthOrdered.May => EDateMonthStartOrdered.May,
            EDateMonthOrdered.June => EDateMonthStartOrdered.June,
            EDateMonthOrdered.July => EDateMonthStartOrdered.July,
            EDateMonthOrdered.August => EDateMonthStartOrdered.August,
            EDateMonthOrdered.September => EDateMonthStartOrdered.September,
            EDateMonthOrdered.October => EDateMonthStartOrdered.October,
            EDateMonthOrdered.November => EDateMonthStartOrdered.November,
            EDateMonthOrdered.December => EDateMonthStartOrdered.December,
            EDateMonthOrdered.January => EDateMonthStartOrdered.January,
            EDateMonthOrdered.February => EDateMonthStartOrdered.February,
            EDateMonthOrdered.March => EDateMonthStartOrdered.March,
            EDateMonthOrdered.MonthOrderedMax => EDateMonthStartOrdered.MonthStartOrderedMax,
            EDateMonthOrdered.EDateMonthOrdered_MAX => EDateMonthStartOrdered.EDateMonthStartOrdered_MAX,
            _ => throw new InvalidEnumArgumentException(nameof(month), (int)month, typeof(EDateMonthOrdered)),
        };
}
public class Month
{
    public string name;
    public EDateMonth dateMonth;
    public EDateMonthOrdered dateMonthOrdered;
    public uint dateDaysInMonth;
    public uint dateDayStart;

    public Month(EDateMonth eDateMonth, uint eDateDaysInMonth)
    {
        name = nameof(eDateMonth);
        dateMonth = eDateMonth;
        var convertMonth = MonthExtensions.GetMonthOrdered(dateMonth);
        dateMonthOrdered = convertMonth;
        dateDaysInMonth = eDateDaysInMonth;
        dateDayStart = ((uint)MonthExtensions.GetMonthStart(convertMonth));
    }
}