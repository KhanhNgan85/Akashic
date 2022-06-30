/*****************************************************************************/
/* Build  : 18-Mar-2016                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.Collections.Specialized;

namespace Akashic.Utilities.Extensions.Collection
{
    public static class NameValueCollectionExtensions
    {
        // int id = request.QueryString.GetValue<int>("id");
        // DateTime date = request.QueryString.GetValue<DateTime>("date");
        public static T GetValue<T>(this NameValueCollection source, string key)
             where T : IConvertible
        {
            var value = source[key];
            var result = value == null ? default : (T)Convert.ChangeType(value, typeof(T));

#pragma warning disable CS8603 // Possible null reference return.
            return result;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
