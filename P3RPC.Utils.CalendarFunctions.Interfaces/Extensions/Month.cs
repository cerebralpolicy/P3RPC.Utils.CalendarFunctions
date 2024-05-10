using System.ComponentModel;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
namespace P3RPC.Utils.CalendarFunctions.Interfaces.Extensions;

public static class MonthEx
{
    public static EDateMonth GetEDateMonth(EDateMonthStartOrdered month)
        => month switch
        {
            EDateMonthStartOrdered.January => EDateMonth.January,
            EDateMonthStartOrdered.February => EDateMonth.February,
            EDateMonthStartOrdered.March => EDateMonth.March,
            EDateMonthStartOrdered.April => EDateMonth.April,
            EDateMonthStartOrdered.May => EDateMonth.May,
            EDateMonthStartOrdered.June => EDateMonth.June,
            EDateMonthStartOrdered.July => EDateMonth.July,
            EDateMonthStartOrdered.August => EDateMonth.August,
            EDateMonthStartOrdered.September => EDateMonth.September,
            EDateMonthStartOrdered.October => EDateMonth.October,
            EDateMonthStartOrdered.November => EDateMonth.November,
            EDateMonthStartOrdered.December => EDateMonth.December,
            EDateMonthStartOrdered.MonthStartOrderedMax => EDateMonth.MonthMax,
            EDateMonthStartOrdered.EDateMonthStartOrdered_MAX => EDateMonth.EDateMonth_MAX,
            _ => throw new InvalidEnumArgumentException(nameof(month), (int)month, typeof(EDateMonthStartOrdered)),
        };
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
            EDateMonth.EDateMonth_MAX => EDateMonthOrdered.EDateMonthOrdered_MAX,
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