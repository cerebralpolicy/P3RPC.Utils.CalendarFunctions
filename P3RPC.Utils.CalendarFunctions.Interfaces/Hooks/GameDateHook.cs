using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using Project.Utils;

namespace P3RPC.Utils.CalendarFunctions.Interfaces.Hooks;

public static class GameDateHook
{
    private const int mar = (int)EDateMonthStartOrdered.March;
    private const int feb = (int)EDateMonthStartOrdered.February;
    private const int jan = (int)EDateMonthStartOrdered.January;
    private const int dec = (int)EDateMonthStartOrdered.December;
    private const int nov = (int)EDateMonthStartOrdered.November;
    private const int oct = (int)EDateMonthStartOrdered.October;
    private const int sep = (int)EDateMonthStartOrdered.September;
    private const int aug = (int)EDateMonthStartOrdered.August;
    private const int jul = (int)EDateMonthStartOrdered.July;
    private const int jun = (int)EDateMonthStartOrdered.June;
    private const int may = (int)EDateMonthStartOrdered.May;
    private const int apr = (int)EDateMonthStartOrdered.April;

    private const EDateMonth m_dec = EDateMonth.December;
    private const EDateMonth m_nov = EDateMonth.November;
    private const EDateMonth m_oct = EDateMonth.October;
    private const EDateMonth m_sep = EDateMonth.September;
    private const EDateMonth m_aug = EDateMonth.August;
    private const EDateMonth m_jul = EDateMonth.July;
    private const EDateMonth m_jun = EDateMonth.June;
    private const EDateMonth m_may = EDateMonth.May;
    private const EDateMonth m_apr = EDateMonth.April;
    private const EDateMonth m_mar = EDateMonth.March;
    private const EDateMonth m_feb = EDateMonth.February;
    private const EDateMonth m_jan = EDateMonth.January;



    internal static bool expect2009 (int month)
    {
        return month > 3;
    }

    public static uint ParseDate (DateOnly date)
    {
        var Year = date.Year;
        var Month = date.Month;
        var Day = date.Day;

        if (expect2009(Month))
        {
            if (Year == 2009)
            {
                var monthLookup = Month - 1;
                var daysElapMonth = Day - 1;
                var monthEnum = (EDateMonth)monthLookup;
                var monthStart = (int)(EDateMonthStartOrdered)monthEnum;
                return (uint)monthStart + (uint)daysElapMonth;
            }
            else
            {
                var errormsg = "Expected a year of 2009.";
                Log.Error(errormsg);
                throw new ArgumentException(errormsg, nameof(date));
            }
        }
        else
        {
            if (Year == 2010)
            {
                var monthLookup = Month - 1;
                var daysElapMonth = Day - 1;
                var monthEnum = (EDateMonth)monthLookup;
                var monthStart = (int)(EDateMonthStartOrdered)monthEnum;
                return (uint)monthStart + (uint)daysElapMonth;
            }
            else
            {
                var errormsg = "Expected a year of 2010.";
                Log.Error(errormsg);
                throw new ArgumentException(errormsg, nameof(date));
            }
        }
    } 

    public static uint Year(uint DaysSinceApril)
    {
        var thisMonth = Month(DaysSinceApril);
        if (thisMonth > 3)
        {
            return 2009;
        }
        else
        {
            return 2010;
        }
    }
    public static uint Day(uint DaysSinceApril)
    {
        var _d = DaysSinceApril;
        uint _sub = 0;
        if (_d > mar)
        {
            _sub = mar;
        }
        else if (_d > feb)
        {
            _sub = feb;
        }
        else if (_d > jan)
        {
            _sub = jan;
        }
        else if (_d > dec)
        {
            _sub = dec;
        }
        else if (_d>nov)
        {
            _sub = nov;
        }
        else if (_d > oct)
        {
            _sub = oct;
        }
        else if (_d > sep)
        {
            _sub = sep;
        }
        else if (_d > aug)
        {
            _sub = aug;
        }
        else if (_d > jul)
        {
            _sub = jul;
        }
        else if (_d > jun)
        {
            _sub = jun;
        }
        else if (_d > may)
        {
            _sub = may;
        }
        else
        {
            _sub = apr;
        }

        return (_d - _sub) + 1;
    }
    public static uint Month(uint DaysSinceApril)
    {
        var _d = DaysSinceApril;
        uint _monthVal = 0;
        if (_d > mar)
        {
            _monthVal = 1 + (int)m_mar;
        }
        else if (_d > feb)
        {
            _monthVal = 1 + (int)m_feb;
        }
        else if (_d > jan)
        {
            _monthVal = 1 + (int)m_jan;
        }
        else if (_d > dec)
        {
            _monthVal = 1 + (int)m_dec;
        }
        else if (_d > nov)
        {
            _monthVal = 1 + (int)m_nov;
        }
        else if (_d > oct)
        {
            _monthVal = 1 + (int)m_oct;
        }
        else if (_d > sep)
        {
            _monthVal = 1 + (int)m_sep;
        }
        else if (_d > aug)
        {
            _monthVal = 1 + (int)m_aug;
        }
        else if (_d > jul)
        {
            _monthVal = 1 + (int)m_jul;
        }
        else if (_d > jun)
        {
            _monthVal = 1 + (int)m_jun;
        }
        else if (_d > may)
        {
            _monthVal = 1 + (int)m_may;
        }
        else
        {
            _monthVal = 1 + (int)m_apr;
        }
        return _monthVal;
    }

    public static string MonthName(uint DaysSinceApril)
    {
        var _d = DaysSinceApril;
        string _monthVal = "";
        if (_d > mar)
        {
            _monthVal = nameof(m_mar);
        }
        else if (_d > feb)
        {
            _monthVal = nameof(m_feb);
        }
        else if (_d > jan)
        {
            _monthVal = nameof(m_jan);
        }
        else if (_d > dec)
        {
            _monthVal = nameof(m_dec);
        }
        else if (_d > nov)
        {
            _monthVal = nameof(m_nov);
        }
        else if (_d > oct)
        {
            _monthVal = nameof(m_oct);
        }
        else if (_d > sep)
        {
            _monthVal = nameof(m_sep);
        }
        else if (_d > aug)
        {
            _monthVal = nameof(m_aug);
        }
        else if (_d > jul)
        {
            _monthVal = nameof(m_jul);
        }
        else if (_d > jun)
        {
            _monthVal = nameof(m_jun);
        }
        else if (_d > may)
        {
            _monthVal = nameof(m_may);
        }
        else
        {
            _monthVal = nameof(m_apr);
        }
        return _monthVal;
    }
}
