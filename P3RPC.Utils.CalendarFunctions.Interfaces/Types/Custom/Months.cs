namespace P3RPC.Utils.CalendarFunctions.Interfaces.Types;


public enum EDateMonth: byte
{
    January = 0,
    February = 1,
    March = 2,
    April = 3,
    May = 4,
    June = 5,
    July = 6,
    August = 7,
    September = 8,
    October = 9,
    November = 10,
    December = 11,
    MonthMax = 12,
    EDateMonth_MAX = 13,
}
public enum EDateMonthOrdered: byte
{
    April = 0,
    May = 1,
    June = 2,
    July = 3,
    August = 4,
    September = 5, 
    October = 6,
    November = 7,
    December = 8,
    January = 9,
    February = 10,
    March = 11,
    MonthOrderedMax = 12,
    EDateMonthOrdered_MAX = 13,
}

public enum EDateMonthStartOrdered
{
    April = 0,
    May = 30,
    June = 61,
    July = 91,
    August = 122,
    September = 153,
    October = 183,
    November = 214,
    December = 244,
    January = 275,
    February = 306,
    March = 334,
    MonthStartOrderedMax = 365,
    EDateMonthStartOrdered_MAX = 366,
}