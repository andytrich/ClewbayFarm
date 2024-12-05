using System;
using System.Globalization;

namespace ClewbayFarmAPI.Utils
{
    public static class DateHelper
    {
        public static DateTime GetDateFromWeekNumber(int year, int weekNumber, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            // Get the first day of the year
            DateTime jan1 = new DateTime(year, 1, 1);

            // Get the ISO 8601 calendar (adjusts for weeks starting on Monday)
            CultureInfo ci = CultureInfo.CurrentCulture;
            Calendar calendar = ci.Calendar;

            // Adjust to the correct first day of the week
            int daysOffset = (int)firstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekStart = jan1.AddDays(daysOffset);

            // Ensure the first week is fully within the year
            int firstWeek = calendar.GetWeekOfYear(firstWeekStart, CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek);
            if (firstWeek != 1)
            {
                firstWeekStart = firstWeekStart.AddDays(7);
            }

            // Add weeks to get the desired week
            DateTime result = firstWeekStart.AddDays((weekNumber - 1) * 7);

            return result;
        }
    }

}
