/*****************************************************************************/
/* Build  : 05-Jun-2013                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/

namespace Akashic.Utilities.Extensions
{
    public static class IntegerExtensions
    {
        public static string ToMonth(this int month, bool fullName = true)
        {
            if (month <= 0 || month >= 13)
                throw new ArgumentException(nameof(month));

            var months = fullName ? 
                new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "Deccember" } : 
                new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var result = months[month - 1];
            return result;
        }

        public static string ToDayOfWeek(this int day, bool fullName = true)
        {
            if (day <= 0 || day >= 8)
                throw new ArgumentException(nameof(day));

            var days = fullName ?
                new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" } :
                new string[] { "Mon", "Tue", "Wed", "Thus", "Fri", "Sat", "Sun" };
            var result = days[day - 1];
            return result;
        }
    }
}
