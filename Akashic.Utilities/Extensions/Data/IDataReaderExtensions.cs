/*****************************************************************************/
/* Build  : 19-Jan-2015                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.Data;

namespace Akashic.Utilities.Extensions.Data
{
    public static class IDataReaderExtensions
    {
        public static bool IsValid(this IDataReader reader)
        {
            var result = (reader != null) && (!reader.IsClosed);
            return result;
        }

        public static T GetField<T>(this IDataReader reader, int index)
            where T : IConvertible
        {
            var value = reader[index];
            var result = value is DBNull || value == null ? 
                default : (T)Convert.ChangeType(value, typeof(T));

#pragma warning disable CS8603 // Possible null reference return.
            return result;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static T GetField<T>(this IDataReader reader, string field)
            where T : IConvertible
        {
            var index = reader.GetOrdinal(field);
            var result = GetField<T>(reader, index);
            return result;
        }
    }
}
