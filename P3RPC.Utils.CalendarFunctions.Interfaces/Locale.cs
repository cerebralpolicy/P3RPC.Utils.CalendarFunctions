using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace P3RPC.Utils.CalendarFunctions.Interfaces
{
    public class Locale
    {
        Type CalendarType { get; set; }

        public string CalendarLocale { get; set; }


        public Locale(DateFormatType formatType)
        {
            CalendarType = typeof(GregorianCalendar);
            CalendarLocale = LocalDateFormat(formatType);
        }

        public string PrintDate (P3Date p3Date)
        {
            var thisDate = p3Date.Date;
            return thisDate.ToString(this.CalendarLocale);
        }

        public string LocalDateFormat(DateFormatType formatType)
            => formatType switch
            {
                DateFormatType.American => "en-US",
                DateFormatType.International => "en-GB",
                DateFormatType.Japanese => "ja-JP",
                _ => throw new NotImplementedException()
            };
    }
}
