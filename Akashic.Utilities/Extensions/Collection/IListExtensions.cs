/*****************************************************************************/
/* Build  : 06-Jan-2015                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.ComponentModel;

namespace Akashic.Utilities.Extensions.Collection
{
    public static class IListExtensions
    {
        public static IList<T> Clone<T>(this IList<T> source) where T : ICloneable
        {
            var result = source.Select(item => (T)item.Clone()).ToList();
            return result;
        }

        public static BindingList<T> ToBindingList<T>(this IList<T> source)
        {
            var result = new BindingList<T>();

            if (source != null && source.Any())
            {
                foreach (T item in source)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
