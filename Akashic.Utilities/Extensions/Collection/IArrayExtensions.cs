/*****************************************************************************/
/* Build  : 03-Mar-2016                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/

namespace Akashic.Utilities.Extensions.Collection
{
    public static class IArrayExtensions
    {
        /// <summary>
        /// Usage: 
        ///     _array.RemoveAt(index);
        /// </summary>
        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            var result = new T[source.Length - 1];

            if (index > 0)
                Array.Copy(source, 0, result, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, result, index, source.Length - index - 1);

            return result;
        }
    }
}
