/*****************************************************************************/
/* Build  : 03-Mar-2016                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.ComponentModel;
using System.Data;

namespace Akashic.Utilities.Extensions.Collection
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Usage: 
        ///     _list.Page(pageIndex, pageSize);
        /// </summary>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int pagesize)
        {
            var result = source.Skip((page - 1) * pagesize).Take(pagesize);
            return result;
        }


        /// <summary>
        /// Usage: 
        ///     _list.DistinctWith(x => x.property).ToList();
        /// </summary> 
        [Obsolete("DistinctWith is deprecated, please use DistinctBy instead.")]
        public static IEnumerable<T> DistinctWith<T, TKey>(this IEnumerable<T> source, Func<T, TKey> property)
        {
            var result = source.GroupBy(property).Select(x => x.First());
            return result;
        }


        /// <summary>
        /// Usage: 
        ///     _list.GetRandom(number);
        /// </summary> 
        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> source, int count = 1)
        {
            var ran = new Random();
            var maxLength = source.Count();
            var result = new List<T>();

            do
            {
                var randomInterval = ran.Next(0, maxLength);
                var elem = source.ElementAt(randomInterval);
                if (!result.Contains(elem))
                {
                    result.Add(elem);
                    count--;
                }
            }
            while (count != 0);

            return result;
        }


        /// <summary>
        /// Usage:
        ///     _list.ToDataTable();
        /// </summary> 
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in source)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;
        }


        /// <summary>
        /// Usage: 
        ///     _array.JoinToString();
        ///     _list.JoinToString();
        /// </summary> 
        public static string JoinToString<T>(this IEnumerable<T> source, char delimiters = ',')
        {
            string result = source!=null && source.Any() ? 
                string.Join($"{delimiters}", source) : string.Empty;
            return result;
        }


        /// <summary>
        /// Usage: 
        ///     _list.IndexOf(new Model() { property1 = value1, property1 = value2,... });
        /// Return: 
        ///     -1 if not exists 
        /// </summary> 
        public static int IndexOf<T>(this IEnumerable<T> source, T value, IEqualityComparer<T>? comparer = null)
        {
            var index = 0;
            comparer ??= EqualityComparer<T>.Default;

            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }

            return -1;
        }


        /// <summary>
        /// Usage: 
        ///     _list.IndexOf(x => x.property == value);
        /// 
        /// Return: 
        ///     -1 if not exists 
        /// </summary> 
        public static int IndexOf<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            int index = 0;

            foreach (T elem in source)
            {
                if (predicate(elem))
                {
                    return index;
                }
                ++index;
            }

            return -1;
        }


        /// <summary>
        /// Usage:
        ///     _list.Exists(x => x.property == value)
        /// </summary> 
        public static bool Exists<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Usage:
        ///     _list.IsValid()
        /// </summary> 
        public static bool IsValid<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }
    }

    public static class ByteIEnumerableExtensions
    {
        public static string ToHexString(this IEnumerable<byte> bytes)
        {
            return string.Concat(bytes.Select(b => b.ToString("x2")).ToArray());
        }
    }
}