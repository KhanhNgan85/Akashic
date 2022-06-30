/*****************************************************************************/
/* Build  : 27-Jun-2022                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/

namespace Akashic.Utilities.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Show full date with format "dd/MM/yyyy hh:mm:ss tt"
        /// </summary>
        public static string ShowFull(this DateTimeOffset dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        /// <summary>
        /// Show full date with format "dd/MM/yyyy hh:mm:ss tt"
        /// </summary>
        public static string ShowFull(this DateTimeOffset? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("dd/MM/yyyy hh:mm:ss tt") : string.Empty;
        }
    }
}
