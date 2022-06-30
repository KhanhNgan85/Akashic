/*****************************************************************************/
/* Build : 09-Mar-2013                                                       */
/* Author : www.kn.team                                                      */
/*****************************************************************************/

namespace Akashic.Utilities.Extensions.Collection
{
    public static class IQueryableExtensions
    {
        //used by LINQ to SQL
        public static IQueryable<T> Page<T>(this IQueryable<T> source, int page, int pageSize)
        {
            var result = source.Skip((page - 1) * pageSize).Take(pageSize);
            return result;
        }
    }
}
