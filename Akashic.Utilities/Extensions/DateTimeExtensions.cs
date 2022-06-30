/*****************************************************************************/
/* Build  : 05-Jun-2015                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.Globalization;

namespace Akashic.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Covert to "dd/MM/yyyy" string
        /// </summary>
        public static string ToVnString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert 29-Sep-1985 to 850929
        /// </summary>
        public static string ToSortCode(this DateTime date)
        {
            string day = date.Day > 9 ? $"{date.Day}" : $"0{date.Day}";
            string month = date.Month > 9 ? $"{date.Month}" : $"0{date.Month}";
            string year = $"{date.Year}".Substring(2);

            return $"{year}{month}{day}";
        }

        /// <summary>
        /// Convert 29-Sep-1985 12:30:1 to 850929123001
        /// </summary>
        public static string ToLongCode(this DateTime date)
        {
            string day = date.Day > 9 ? $"{date.Day}" : $"0{date.Day}";
            string month = date.Month > 9 ? $"{date.Month}" : $"0{date.Month}";
            string year = $"{date.Year}".Substring(2);

            string hour = date.Hour > 9 ? $"{date.Hour}" : $"0{date.Hour}";
            string minute = date.Minute > 9 ? $"{date.Minute}" : $"0{date.Minute}";
            string second = date.Second > 9 ? $"{date.Second}" : $"0{date.Second}";

            return $"{year}{month}{day}{hour}{minute}{second}";
        }

        //

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static int DaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.DaysInMonth());
        }
    }
}
